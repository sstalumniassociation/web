<script setup lang="ts">
import dayjs from 'dayjs'
import type { EventWithAttendees, Event } from '~/shared/types'
import { useMutation, useQueryClient } from '@tanstack/vue-query'
import { ref } from 'vue'

const props = defineProps<{
  event: EventWithAttendees
  showPopup: boolean
}>()
const queryClient = useQueryClient()

const mutation = useMutation({
    mutationFn: (id: string) => $api(`/api/event/${id}`, {
        method: 'PUT',
        body: changedEvent.value,
    }),
})
const msg = ref('')

const closePopup = defineEmits(['closePopup'])

const state = reactive({
  ...props.event,
  startDateTime: dayjs(parseInt(props.event.startDateTime) * 1000).format("YYYY-MM-DDTHH:mm"),
  endDateTime: dayjs(parseInt(props.event.endDateTime) * 1000).format("YYYY-MM-DDTHH:mm"),
})
const changedEvent = ref<Partial<Event>>({})

const handleSubmit = () => {
  // Check differences between original and edited
  for (const key in props.event) {
    if (key === "startDateTime" || key === "endDateTime") {
      if (parseInt(props.event[key as keyof Event]) !== dayjs(state[key as keyof Event]).unix()) {
        changedEvent.value[key as keyof Event] = state[key as keyof Event]
      }
    } else {
      if (props.event[key as keyof Event] !== state[key as keyof Event]) {
        changedEvent.value[key as keyof Event] = state[key as keyof Event]
      }
    }
  }

  // Config Data
  if (changedEvent.value.startDateTime) changedEvent.value.startDateTime = dayjs(parseInt(changedEvent.value.startDateTime) * 1000).toISOString()
  if (changedEvent.value.endDateTime) changedEvent.value.endDateTime = dayjs(parseInt(changedEvent.value.endDateTime) * 1000).toISOString()

  if (Object.keys(changedEvent.value).length === 0) {
    msg.value = "Nothing to update!"
    return
  }

  // Send Data
  mutation.mutate(props.event.id)

  msg.value = "Update Successful!"
  queryClient.invalidateQueries({ queryKey: ['events'] })
}
</script>

<template>
  <div>
    <UModal v-model="props.showPopup">
      <UCard>
        <template #header>
          <div class="flex flex-row items-start">
            <div>
              <h1 class="text-xl font-bold pb-1">
                Update Event
              </h1>
              <p class="text-md">
                Update information regarding {{ props.event.name }}. Note that Event ID cannot be changed.
              </p>
            </div>
            <UButton color="gray" variant="ghost" icon="i-heroicons-x-mark-20-solid" class="ml-auto -my-1" @click="$emit('closePopup')" />
          </div>
        </template>

        <UForm :state="state" class="space-y-7" @submit="handleSubmit">
          <div class="space-y-5">
            <UFormGroup label="Event Name" required>
              <UInput v-model="state.id" color="gray" variant="outline" disabled />
            </UFormGroup>
            <UFormGroup label="Event Name" required :error="!state.name && 'You must enter an Event Name!'">
              <UInput v-model="state.name" placeholder="Amazing Event" color="gray" variant="outline" />
            </UFormGroup>
            <UFormGroup label="Description" description="Provide a short description describing what this event is about" required :error="!state.description && 'You must enter an Event Description!'">
              <UInput v-model="state.description" placeholder="Description" color="gray" variant="outline" />
            </UFormGroup>
            <UFormGroup label="Location" description="Where is this event held at" required :error="!state.location && 'You must enter the Event Location!'">
              <UInput v-model="state.location" placeholder="School of Science and Technology, Singapore" color="gray" variant="outline" />
            </UFormGroup>
            <UFormGroup name="imageurl" label="Banner Image" description="The image shown to everyone viewing this event. Enter a valid Image URL." required :error="!state.badgeImage && 'You must enter a valid Image URL'">
              <UInput v-model="state.badgeImage" name="imageurl" type="url" placeholder="https://example.com" color="gray" variant="outline" />
            </UFormGroup>
            <UFormGroup label="Start Date and Time" required :error="!state.startDateTime && 'You must enter the Start Date!'">
              <UInput v-model="state.startDateTime" type="datetime-local" color="gray" variant="outline" />
            </UFormGroup>
            <UFormGroup label="End Date and Time" required :error="!state.endDateTime && 'You must enter the End Date!'">
              <UInput v-model="state.endDateTime" type="datetime-local" color="gray" variant="outline" />
            </UFormGroup>
          </div>

          <UButton v-if="!msg.includes('Successful')" type="submit" color="green">
            Update Event
          </UButton>
          <p v-if="msg !== ''">{{ msg }}</p>
        </UForm>
        <UButton v-if="msg.includes('Successful')" color="green" @click="$emit('closePopup')">
          Close
        </UButton>
      </UCard>
    </UModal>
  </div>
</template>
