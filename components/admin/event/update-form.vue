<script setup lang="ts">
import { z } from 'zod'

const props = defineProps<{
  name: string
  description: string | null
  location: string | null
  badgeImage: string | null
  startDateTime: string
  endDateTime: string
}>()

const { $dayjs } = useNuxtApp()

const state = reactive({
  name: props.name,
  description: props.description,
  location: props.location,
  badgeImage: props.badgeImage,
  startDateTime: props.startDateTime,
  endDateTime: props.endDateTime,
})

const schema = z.object({
  name: z.string()
    .min(1, 'Please enter a name'),
  description: z.string(),
  location: z.string(),
  badgeImage: z.string()
    .url('Please enter a URL'),
  startDateTime: z.string()
    .transform(d => $dayjs(d).toISOString())
    .refine(val => $dayjs(val).isAfter($dayjs()), 'Start date must be in the future'),
  endDateTime: z.string()
    .transform(d => $dayjs(d).toISOString()),
}).refine((val) => {
  return $dayjs(val.startDateTime).isBefore(val.endDateTime)
}, {
  message: 'Event must end after it starts',
  path: ['endDateTime'],
})
</script>

<template>
  <UForm :state="state" :schema="schema" class="space-y-7">
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

    <UButton type="submit">
      Update
    </UButton>
  </UForm>
</template>
