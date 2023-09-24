<script setup lang="ts">
import { ref } from "vue"
import { f7Link, f7NavRight, f7Navbar, f7Page, f7Popup, f7List, f7ListInput, f7Button } from 'framework7-vue';

const event = reactive({
    id: '',
    name: '',
    description: '',
    location: '',
    badgeImage: '',
    startDateTime: '',
    endDateTime: '',
})

const pending = ref(false)
const popupOpened = ref(false)

async function createEvent() {
    pending.value = true

    // try {
    //     const res = await $api("/api/event/", {
    //         method: "POST",
    //         body: {
    //             ...event
    //         }
    //     })
    // } catch (err) {

    // }

    popupOpened.value = false
}
</script>

<template>
    <f7Popup v-model:opened:="popupOpened" class="create-event-popup">
        <f7Page>
            <f7Navbar title="Create Event">
                <f7NavRight>
                    <f7Link popup-close>Close</f7Link>
                </f7NavRight>
            </f7Navbar>
            <f7Page>
                <f7List form @submit.prevent="createEvent">
                    <f7ListInput v-model:value="event.name" label="Name" placeholder="What is your event called?" required/>
                    <f7ListInput v-model:value="event.description" label="Description" placeholder="Short description of your event" required/>
                    <f7ListInput v-model:value="event.location" label="Location" placeholder="Where is it held?" required/>
                    <f7ListInput v-model:value="event.badgeImage" type="url" label="Image URL" placeholder="https://example.com" required validate />
                    <f7ListInput v-model:value="event.startDateTime" type="datetime-local" label="Start Date and Time" placeholder="When does your event start" required/>
                    <f7ListInput v-model:value="event.endDateTime" type="datetime-local" label="End Date and Time" placeholder="When does your event end" required/>

                    <f7List inset>
                        <f7Button fill type="submit" preloader :loading="pending" :disabled="pending">
                            Done
                        </f7Button>
                    </f7List>
                </f7List>
            </f7Page>
        </f7Page>
    </f7Popup>
</template>