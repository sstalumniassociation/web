<script setup lang="ts">
import dayjs from 'dayjs'
import { useMutation, useQueryClient } from '@tanstack/vue-query'
import { ref } from 'vue'
import type { Event, EventWithAttendees } from '~/shared/types'

const props = defineProps<{
  event: EventWithAttendees
}>()
const visible = defineModel<boolean>()
const queryClient = useQueryClient()
const msg = ref('')

const state = reactive({
  ...props.event,
  startDateTime: dayjs(Number.parseInt(props.event.startDateTime) * 1000).format('YYYY-MM-DDTHH:mm'),
  endDateTime: dayjs(Number.parseInt(props.event.endDateTime) * 1000).format('YYYY-MM-DDTHH:mm'),
})
const changedEvent = ref<Partial<Event>>({})

const mutation = useMutation({
  mutationFn: (id: string) => $api(`/api/event/${id}`, {
    method: 'PUT',
    body: changedEvent.value,
  }),
  onSuccess: () => {
    msg.value = 'Update Successful!'
    queryClient.invalidateQueries({ queryKey: ['events'] })
  },
})

function handleSubmit() {
  // Config Data
  changedEvent.value = { ...state }
  if (changedEvent.value.startDateTime)
    changedEvent.value.startDateTime = dayjs(Number.parseInt(changedEvent.value.startDateTime) * 1000).toISOString()
  if (changedEvent.value.endDateTime)
    changedEvent.value.endDateTime = dayjs(Number.parseInt(changedEvent.value.endDateTime) * 1000).toISOString()

  if (Object.keys(changedEvent.value).length === 0) {
    msg.value = 'Nothing to update!'
    return
  }

  // Send Data
  mutation.mutate(props.event.id)
}
</script>

<template>
  <div>
    <UModal v-model="visible">
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
            <UButton color="gray" variant="ghost" icon="i-heroicons-x-mark-20-solid" class="ml-auto -my-1" @click="() => visible = false" />
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

          <UButton v-if="!mutation.isSuccess.value" type="submit" color="green">
            Update Event
          </UButton>
          <p v-if="msg !== ''">
            {{ msg }}
          </p>
        </UForm>
        <UButton v-if="mutation.isSuccess.value" color="green" @click="() => visible = false">
          Close
        </UButton>
      </UCard>
    </UModal>
  </div>
</template>
