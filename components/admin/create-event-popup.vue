<script setup lang="ts">
import dayjs from 'dayjs'
import { ref } from 'vue'
import { useMutation, useQueryClient } from '@tanstack/vue-query'
import type { Event } from '~/shared/types'

const visible = defineModel<boolean>()
const queryClient = useQueryClient()

const state = reactive({
  name: '',
  description: '',
  location: '',
  badgeImage: '',
  startDateTime: '',
  endDateTime: '',
  formSubmitted: false,
})
const newEventId = ref('')
const inputError = ref('')

const { mutate: createEventMutate, isPending: createEventIsPending } = useCreateEventMutation()

async function handleSubmit() {
  state.formSubmitted = true
  inputError.value = ''

  const newEvent = { ...state }
  newEvent.startDateTime = dayjs(state.startDateTime).toISOString()
  newEvent.endDateTime = dayjs(state.endDateTime).toISOString()

  try {
    const res = await mutation.mutateAsync(newEvent)

    newEventId.value = res.id
    queryClient.invalidateQueries({ queryKey: ['events'] })
  }
  catch (err) {
    if (err instanceof Error && err.message.includes('Invalid Image URL'))
      inputError.value = 'Invalid Image URL'
  }
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
                Create Event
              </h1>
              <p class="text-md">
                Create a new SSTAA event
              </p>
            </div>
            <UButton color="gray" variant="ghost" icon="i-heroicons-x-mark-20-solid" class="ml-auto -my-1" @click="() => visible = false" />
          </div>
        </template>

        <UForm :state="state" class="space-y-7" @submit="handleSubmit">
          <div class="space-y-5">
            <UFormGroup label="Event Name" required :error="(!state.name && state.formSubmitted) && 'You must enter an Event Name!'">
              <UInput v-model="state.name" placeholder="Amazing Event" color="gray" variant="outline" :disabled="newEventId !== ''" />
            </UFormGroup>
            <UFormGroup label="Description" description="Provide a short description describing what this event is about" required :error="(!state.description && state.formSubmitted) && 'You must enter an Event Description!'">
              <UInput v-model="state.description" placeholder="Description" color="gray" variant="outline" :disabled="newEventId !== ''" />
            </UFormGroup>
            <UFormGroup label="Location" description="Where is this event held at" required :error="(!state.location && state.formSubmitted) && 'You must enter the Event Location!'">
              <UInput v-model="state.location" placeholder="School of Science and Technology, Singapore" color="gray" variant="outline" :disabled="newEventId !== ''" />
            </UFormGroup>
            <UFormGroup name="imageurl" label="Banner Image" description="The image shown to everyone viewing this event. Enter a valid Image URL." required :error="(!state.badgeImage && state.formSubmitted) && 'You must enter a valid Image URL'">
              <UInput v-model="state.badgeImage" name="imageurl" type="url" placeholder="https://example.com" color="gray" variant="outline" :disabled="newEventId !== ''" />
            </UFormGroup>
            <UFormGroup label="Start Date and Time" required :error="(!state.startDateTime && state.formSubmitted) && 'You must enter the Start Date!'">
              <UInput v-model="state.startDateTime" type="datetime-local" color="gray" variant="outline" :disabled="newEventId !== ''" />
            </UFormGroup>
            <UFormGroup label="End Date and Time" required :error="(!state.endDateTime && state.formSubmitted) && 'You must enter the End Date!'">
              <UInput v-model="state.endDateTime" type="datetime-local" color="gray" variant="outline" :disabled="newEventId !== ''" />
            </UFormGroup>
          </div>

          <UButton v-if="!newEventId" type="submit" color="green" :loading="createEventIsPending">
            Create Event
          </UButton>
          <p v-if="!inputError">
            {{ inputError }}
          </p>
        </UForm>

        <div v-if="mutation.isSuccess.value" class="space-y-3 mt-4">
          <p>Created Event with an ID of <b>{{ newEventId }}</b>.</p>
          <UButton color="blue" @click="() => visible = false">
            Done
          </UButton>
        </div>
      </UCard>
    </UModal>
  </div>
</template>
