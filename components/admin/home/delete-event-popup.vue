<script setup lang="ts">
import { f7Popup, f7Page, f7Navbar, f7NavRight, f7ListInput, f7List, f7Button, f7Link, f7Block, f7BlockTitle } from 'framework7-vue';

const props = defineProps(["eventId", "eventName"])

const state = reactive({
    pending: false,
    verification: "",
    msg: "",
    isError: false,
})

async function deleteEvent() {
    state.pending = true

    if (state.verification !== props.eventId) {
        state.msg = "Verification failed! Ensure you typed the Event ID correctly."
        state.pending = false
        return
    }

    try {
        const res = await $api(`/api/event/${props.eventId}`, {
            method: "DELETE"
        })

        console.log(res)
        state.msg = "Delete successful! You can close this popup now."
    } catch (err) {
        state.isError = true
    }

    state.pending = false
}

</script>

<template>
    <f7Popup class="delete-event-popup">
        <f7Page>
            <f7Navbar title="Delete Event">
                <f7NavRight>
                    <f7Link popup-close="">Cancel</f7Link>
                </f7NavRight>
            </f7Navbar>
            <f7Page>
                <f7BlockTitle large>{{ `Delete Event: ${ props.eventName }` }}</f7BlockTitle>
                <f7Block>
                    <p class="text-4">This event will be <b>permanently deleted</b>, including the record of the attendees who attended the event.</p>
                </f7Block>
                <f7List form @submit.prevent="deleteEvent">
                    <f7ListInput v-model:value="state.verification" :label="`To verify, type the Event ID (${props.eventId}) in the field below:`" :placeholder="props.eventId" required validate :pattern="props.eventId" :disabled="state.msg.includes('successful')" />

                    <f7List inset>
                        <f7Button v-if="!state.isError" fill type="submit" preloader :loading="state.pending" :disabled="state.pending">
                            Continue
                        </f7Button>
                        <f7Button v-if="state.isError" fill popup-close="">
                            Close
                        </f7Button>
                        <p class="text-center">{{ state.msg }}</p>
                    </f7List>
                </f7List>
            </f7Page>
        </f7Page>
    </f7Popup>
</template>