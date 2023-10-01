<script setup lang="ts">
import { useMutation } from '@tanstack/vue-query'
import { watch } from 'vue'
import dayjs from 'dayjs'
import { f7Block, f7BlockTitle, f7Button, f7Link, f7List, f7ListInput, f7NavRight, f7Navbar, f7Page, f7Popup } from 'framework7-vue'
import type { EventWithAttendees } from '~/shared/types'

const props = defineProps<{
  event: EventWithAttendees
}>()

const updatedValues = reactive({
  name: props.event.name,
  description: props.event.description,
  location: props.event.location,
  badgeImage: props.event.badgeImage,
  startDateTime: dayjs(props.event.startDateTime).format('YYYY-MM-DDTHH:mm'),
  endDateTime: dayjs(props.event.endDateTime).format('YYYY-MM-DDTHH:mm'),
})

watch(props, () => {
  updatedValues.startDateTime = dayjs(props.event.startDateTime).format('YYYY-MM-DDTHH:mm')
  updatedValues.endDateTime = dayjs(props.event.endDateTime).format('YYYY-MM-DDTHH:mm')
})

const mutation = useMutation({
  mutationFn: (id: string) => $api(`/api/event/${id}` as '/api/event/:id', {
    method: 'PUT',
  }),
})

async function updateEvent() {
  mutation.mutate(props.event.id)
}
</script>

<template>
  <f7Popup class="update-event-popup">
    <f7Page>
      <f7Navbar title="Update Event">
        <f7NavRight>
          <f7Link popup-close>
            Cancel
          </f7Link>
        </f7NavRight>
      </f7Navbar>
      <f7Page>
        <f7BlockTitle large>
          {{ `Update Event: ${props.event.name}` }}
        </f7BlockTitle>
        <f7Block>
          Some content goes here
        </f7Block>
        <f7List form @submit.prevent="updateEvent">
          <f7ListInput :placeholder="props.event.id" label="Event ID" disabled />
          <f7ListInput v-model:value="updatedValues.name" label="Event Name" :placeholder="props.event.name" />
          <f7ListInput v-model:value="updatedValues.description" label="Event Description" :placeholder="props.event.description" />
          <f7ListInput v-model:value="updatedValues.location" label="Event Location" :placeholder="props.event.location" />
          <f7ListInput v-model:value="updatedValues.badgeImage" label="Image URL" :placeholder="props.event.badgeImage" type="url" />
          <f7ListInput v-model:value="updatedValues.startDateTime" label="Start Date and Time" :placeholder="props.event.startDateTime.toString()" type="datetime-local" />
          <f7ListInput v-model:value="updatedValues.endDateTime" label="End Date and Time" :placeholder="props.event.endDateTime.toString()" type="datetime-local" />

          <f7List inset>
            <f7Button v-if="!mutation.isSuccess.value" fill type="submit" preloader :loading="mutation.isLoading.value" :disabled="mutation.isLoading.value">
              Update Event
            </f7Button>
            <f7Button v-if="mutation.isSuccess.value" fill popup-close>
              Close
            </f7Button>
          </f7List>
        </f7List>
      </f7Page>
    </f7Page>
  </f7Popup>
</template>
