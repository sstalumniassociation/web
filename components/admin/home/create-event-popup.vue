<script setup lang="ts">
import { f7Button, f7Link, f7List, f7ListInput, f7NavRight, f7Navbar, f7Page, f7Popup } from 'framework7-vue'
import { useMutation } from '@tanstack/vue-query'
import { ref } from 'vue'
import type { Event } from '~/shared/types'

const event = reactive({
  name: '',
  description: '',
  location: '',
  badgeImage: '',
  startDateTime: '',
  endDateTime: '',
})

const mutation = useMutation({
  mutationFn: (newEvent: Omit<Event, 'id'>) => $api('/api/event', {
    method: 'POST',
    body: {
      ...newEvent,
    },
  }),
})

const newEventId = ref('')

async function createEvent() {
  event.startDateTime = new Date(event.startDateTime).toISOString()
  event.endDateTime = new Date(event.endDateTime).toISOString()
  const res = await mutation.mutateAsync(event)

  newEventId.value = res.id
}

function resetRefs() {
  newEventId.value = ''
  mutation.reset()
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
          <f7ListInput v-model:value="event.name" label="Name" placeholder="What is your event called?" required :disabled="mutation.isSuccess.value" />
          <f7ListInput v-model:value="event.description" label="Description" placeholder="Short description of your event" required :disabled="mutation.isSuccess.value" />
          <f7ListInput v-model:value="event.location" label="Location" placeholder="Where is it held?" required :disabled="mutation.isSuccess.value" />
          <f7ListInput v-model:value="event.badgeImage" type="url" label="Image URL" placeholder="https://example.com" required validate :disabled="mutation.isSuccess.value" />
          <f7ListInput v-model:value="event.startDateTime" type="datetime-local" label="Start Date and Time" placeholder="When does your event start" required :disabled="mutation.isSuccess.value" />
          <f7ListInput v-model:value="event.endDateTime" type="datetime-local" label="End Date and Time" placeholder="When does your event end" required :disabled="mutation.isSuccess.value" />

          <f7List inset>
            <f7Button v-if="!mutation.isSuccess.value" fill type="submit" preloader :loading="mutation.isLoading.value" :disabled="mutation.isLoading.value">
              Done
            </f7Button>
            <f7Button v-if="mutation.isSuccess.value || mutation.isError.value" fill popup-close @click="resetRefs">
              Close
            </f7Button>
            <p v-if="mutation.isSuccess.value" class="text-center">
              Successfully created event with ID <b>{{ newEventId }}</b>! You may now close the popup.
            </p>
            <p v-if="mutation.isError.value" class="text-center">
              Something went wrong... Please try again later.
            </p>
          </f7List>
        </f7List>
      </f7Page>
    </f7Page>
  </f7Popup>
</template>
