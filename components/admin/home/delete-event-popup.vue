<script setup lang="ts">
import { f7Popup, f7Page, f7Navbar, f7NavRight, f7ListInput, f7List, f7Button, f7Link, f7Block, f7BlockTitle } from 'framework7-vue';

const props = defineProps(["event"])

const state = reactive({
    pending: false,
    verification: "",
    msg: "",
    isDeleted: false,
})

async function deleteEvent() {
    state.pending = true

    try {
        await $api(`/api/event/${props.event.id}`, {
            method: "DELETE"
        })
    } catch (err) {
        console.error(`Deleting Event (${props.event.name}) failed.`)
    }

    state.pending = false
    state.isDeleted = true
}

function resetRefs() {
    state.pending = false
    state.verification = ""
    state.msg = ""
    state.isDeleted = false
}

</script>

<template>
    <f7Popup class="delete-event-popup">
        <f7Page>
            <f7Navbar title="Delete Event">
                <f7NavRight>
                    <f7Link popup-close>Cancel</f7Link>
                </f7NavRight>
            </f7Navbar>
            <f7Page>
                <f7BlockTitle large>{{ `Delete Event: ${ props.event.name }` }}</f7BlockTitle>
                <f7Block>
                    <p class="text-4">This event will be <b>permanently deleted</b>, including the record of the attendees who attended the event.</p>
                </f7Block>
                <f7List form @submit.prevent="deleteEvent">
                    <f7ListInput v-model:value="state.verification" :label="`To verify, type the Event Name (${props.event.name}) in the field below:`" :placeholder="props.event.name" required validate :pattern="props.event.name" :disabled="state.isDeleted" />

                    <f7List inset>
                        <f7Button v-if="!state.isDeleted" fill type="submit" preloader :loading="state.pending" :disabled="state.pending">
                            Continue
                        </f7Button>
                        <f7Button v-if="state.isDeleted" fill popup-close @click="resetRefs">
                            Close
                        </f7Button>
                        <p class="text-center" v-if="state.isDeleted">Delete successful! You can close this popup now.</p>
                    </f7List>
                </f7List>
            </f7Page>
        </f7Page>
    </f7Popup>
</template>