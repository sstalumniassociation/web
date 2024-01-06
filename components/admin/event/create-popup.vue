<script setup lang="ts">
import type { FormSubmitEvent } from '@nuxt/ui/dist/runtime/types'
import { z } from 'zod'

const visible = defineModel<boolean>('visible')
const { $dayjs } = useNuxtApp()
const toast = useToast()

const schema = z.object({
  name: z.string()
    .min(1, 'Please enter a name'),
  description: z.string(),
  location: z.string(),
  badgeImage: z.string()
    .url('Please enter a URL'),
  startDateTime: z.string()
    .refine(val => $dayjs(val).isAfter($dayjs()), 'Start date must be in the future')
    .transform(d => $dayjs(d).unix().toString()),
  endDateTime: z.string()
    .transform(d => $dayjs(d).unix().toString()),
}).refine((val) => {
  return $dayjs(val.startDateTime).isBefore(val.endDateTime)
}, {
  message: 'Event must end after it starts',
  path: ['endDateTime'],
})

type Schema = z.output<typeof schema>

const state = reactive({
  name: '',
  description: '',
  location: '',
  badgeImage: '',
  startDateTime: '',
  endDateTime: '',
})

const { mutate: createEventMutate, isPending: createEventIsPending } = useCreateEventMutation()

function createEvent({ data }: FormSubmitEvent<Schema>) {
  createEventMutate(data, {
    onSuccess() {
      visible.value = false
    },
    onError(err) {
      toast.add({
        title: err.message,
      })
    },
  })
}
</script>

<template>
  <UModal v-model="visible">
    <UCard>
      <template #header>
        <div class="flex justify-between items-start">
          <div>
            <h1 class="text-xl font-semibold">
              Create event
            </h1>
            <p class="text-sm opacity-80">
              Add a new event hosted by SSTAA!
            </p>
          </div>
          <UButton color="gray" variant="ghost" icon="i-heroicons-x-mark-20-solid" @click="visible = false" />
        </div>
      </template>

      <UForm :state="state" :schema="schema" class="space-y-7" @submit="createEvent">
        <div class="space-y-5">
          <UFormGroup label="Name" name="name">
            <UInput v-model="state.name" placeholder="SST Homecoming" />
          </UFormGroup>
          <UFormGroup label="Description" name="description">
            <UTextarea
              v-model="state.description"
              placeholder="Come back to 1 Technology Drive for our inaugural homecoming!"
            />
          </UFormGroup>
          <UFormGroup label="Location" name="location">
            <UInput v-model="state.location" placeholder="1 Technology Drive, Singapore" />
          </UFormGroup>
          <UFormGroup label="Badge image" name="badgeImage">
            <UInput v-model="state.badgeImage" type="url" placeholder="https://app.sstaa.org/cdn/logo.png" />
          </UFormGroup>
          <UFormGroup label="Start" name="startDateTime">
            <UInput v-model="state.startDateTime" after="" type="datetime-local" />
          </UFormGroup>
          <UFormGroup label="End" name="endDateTime">
            <UInput v-model="state.endDateTime" type="datetime-local" />
          </UFormGroup>
        </div>

        <UButton type="submit" :loading="createEventIsPending">
          Create event
        </UButton>
      </UForm>
    </UCard>
  </UModal>
</template>
