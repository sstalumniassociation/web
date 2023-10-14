<script setup lang="ts">
import dayjs from 'dayjs'
import { ref } from 'vue'
import { useMutation } from '@tanstack/vue-query'
import type { FormError } from '@nuxt/ui/dist/runtime/types'
import type { Event } from '~/shared/types'

const props = defineProps<{
  showPopup: boolean
}>()

const closePopup = defineEmits(['closePopup'])
const state = reactive({
  name: '',
  description: '',
  location: '',
  badgeImage: '',
  startDateTime: '',
  endDateTime: '',
})
const newEventId = ref('')
const inputError = ref('')

const mutation = useMutation({
  mutationFn: (newEvent: Omit<Event, 'id'>) => $api('/api/event', {
    method: 'POST',
    body: newEvent,
  }),
})

async function validate(state: any): Promise<FormError[]> {
  const errors: FormError[] = []

  const promise = new Promise((resolve, reject) => {
    const xhttp = new XMLHttpRequest()
    xhttp.open('HEAD', state.badgeImage)
    xhttp.onreadystatechange = async function () {
      if (this.readyState == this.DONE) {
        try {
          if (this.getResponseHeader('Content-Type') !== 'image/png')
            errors.push({ path: 'imageurl', message: 'URL provided must be an image!' })
        }
        catch (e) {
          errors.push({ path: 'imageurl', message: 'URL provided must be an image!' })
        }
        resolve(errors)
      }
    }
    xhttp.send()
  })

  // Wait for the promise to resolve before returning
  await promise

  // Return the errors array
  return errors
}

async function validateURL(url: string) {
  let isError = false
  return new Promise((resolve, reject) => {
    const xhttp = new XMLHttpRequest()
    xhttp.open('HEAD', url)
    xhttp.onreadystatechange = async function () {
      if (this.readyState == this.DONE) {
        try {
          if (this.getResponseHeader('Content-Type') !== 'image/png')
            isError = true
        }
        catch (e) {
          isError = true
        }
        resolve(isError)
      }
    }
    xhttp.send()
  })
}

async function handleSubmit() {
  const urlValidationErr = await validateURL(state.badgeImage)
  if (urlValidationErr) {
    inputError.value = 'URL must be an Image!'
    return
  }

  inputError.value = ''

  const newEvent = state
  newEvent.startDateTime = dayjs(state.startDateTime).toISOString()
  newEvent.endDateTime = dayjs(state.endDateTime).toISOString()

  const res = await mutation.mutateAsync(newEvent)
  newEventId.value = res.id
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
                Create Event
              </h1>
              <p class="text-md">
                Create a new SSTAA event
              </p>
            </div>
            <UButton color="gray" variant="ghost" icon="i-heroicons-x-mark-20-solid" class="ml-auto -my-1" @click="$emit('closePopup')" />
          </div>
        </template>

        <UForm :state="state" class="space-y-7" @submit="handleSubmit">
          <div class="space-y-5">
            <UFormGroup label="Event Name" required :error="!state.name && 'You must enter an Event Name!'">
              <UInput v-model="state.name" placeholder="Amazing Event" color="gray" variant="outline" :disabled="newEventId !== ''" />
            </UFormGroup>
            <UFormGroup label="Description" description="Provide a short description describing what this event is about" required :error="!state.description && 'You must enter an Event Description!'">
              <UInput v-model="state.description" placeholder="Description" color="gray" variant="outline" :disabled="newEventId !== ''" />
            </UFormGroup>
            <UFormGroup label="Location" description="Where is this event held at" required :error="!state.location && 'You must enter the Event Location!'">
              <UInput v-model="state.location" placeholder="School of Science and Technology, Singapore" color="gray" variant="outline" :disabled="newEventId !== ''" />
            </UFormGroup>
            <UFormGroup name="imageurl" label="Banner Image" description="The image shown to everyone viewing this event. Enter a valid Image URL." required :error="!state.badgeImage && 'You must enter a valid Image URL'">
              <UInput v-model="state.badgeImage" name="imageurl" type="url" placeholder="https://example.com" color="gray" variant="outline" :disabled="newEventId !== ''" />
            </UFormGroup>
            <UFormGroup label="Start Date and Time" required :error="!state.startDateTime && 'You must enter the Start Date!'">
              <UInput v-model="state.startDateTime" type="datetime-local" color="gray" variant="outline" :disabled="newEventId !== ''" />
            </UFormGroup>
            <UFormGroup label="End Date and Time" required :error="!state.endDateTime && 'You must enter the End Date!'">
              <UInput v-model="state.endDateTime" type="datetime-local" color="gray" variant="outline" :disabled="newEventId !== ''" />
            </UFormGroup>
          </div>

          <UButton v-if="newEventId === ''" type="submit" color="green" :loading="mutation.isLoading.value">
            Create Event
          </UButton>
          <p v-if="inputError !== ''">
            {{ inputError }}
          </p>
        </UForm>
        <div v-if="mutation.isSuccess.value" class="space-y-3 mt-4">
          <p>Created Event with an ID of <b>{{ newEventId }}</b>.</p>
          <UButton color="blue" @click="$emit('closePopup')">
            Done
          </UButton>
        </div>
      </UCard>
    </UModal>
  </div>
</template>
