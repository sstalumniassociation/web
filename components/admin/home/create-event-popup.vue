<script setup lang="ts">
import { f7Button, f7Link, f7List, f7ListInput, f7NavRight, f7Navbar, f7Page, f7Popup } from 'framework7-vue'

const event = reactive({
  name: '',
  description: '',
  location: '',
  badgeImage: '',
  startDateTime: '',
  endDateTime: '',
})

const state = reactive({
  pending: false,
  isCreated: false,
  isError: false,
  newEventId: '',
})

async function createEvent() {
  state.pending = true
  try {
    event.startDateTime = new Date(event.startDateTime).toISOString()
    event.endDateTime = new Date(event.endDateTime).toISOString()
    const res = await $api('/api/event/', {
      method: 'POST',
      body: {
        ...event,
      },
    })

    state.newEventId = res.id
    state.isCreated = true
  }
  catch (err) {
    state.isError = true
  }
}

function resetRefs() {
  state.pending = false
  state.isCreated = false
  state.isError = false
  state.newEventId = ''
}
</script>

<template>
  <f7Popup class="create-event-popup">
    <f7Page>
      <f7Navbar title="Create Event">
        <f7NavRight>
          <f7Link popup-close>
            Close
          </f7Link>
        </f7NavRight>
      </f7Navbar>
      <f7Page>
        <f7List form @submit.prevent="createEvent">
          <f7ListInput v-model:value="event.name" label="Name" placeholder="What is your event called?" required :disabled="state.isCreated" />
          <f7ListInput v-model:value="event.description" label="Description" placeholder="Short description of your event" required :disabled="state.isCreated" />
          <f7ListInput v-model:value="event.location" label="Location" placeholder="Where is it held?" required :disabled="state.isCreated" />
          <f7ListInput v-model:value="event.badgeImage" type="url" label="Image URL" placeholder="https://example.com" required validate :disabled="state.isCreated" />
          <f7ListInput v-model:value="event.startDateTime" type="datetime-local" label="Start Date and Time" placeholder="When does your event start" required :disabled="state.isCreated" />
          <f7ListInput v-model:value="event.endDateTime" type="datetime-local" label="End Date and Time" placeholder="When does your event end" required :disabled="state.isCreated" />

          <f7List inset>
            <f7Button v-if="!state.isCreated" fill type="submit" preloader :loading="state.pending" :disabled="state.pending">
              Done
            </f7Button>
            <f7Button v-if="state.isCreated || state.isError" fill popup-close @click="resetRefs">
              Close
            </f7Button>
            <p v-if="state.isCreated" class="text-center">
              Successfully created event with ID <b>{{ state.newEventId }}</b>! You may now close the popup.
            </p>
            <p v-if="state.isError" class="text-center">
              Something went wrong... Please try again later.
            </p>
          </f7List>
        </f7List>
      </f7Page>
    </f7Page>
  </f7Popup>
</template>
