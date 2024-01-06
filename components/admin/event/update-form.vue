<script setup lang="ts">
import type { FormSubmitEvent } from '@nuxt/ui/dist/runtime/types'
import { z } from 'zod'

const props = defineProps<{
  id: string
  name: string
  description?: string | null
  location?: string | null
  badgeImage?: string | null
  startDateTime: number
  endDateTime: number
}>()

const toast = useToast()
const dayjs = useDayjs()

const state = reactive({
  name: props.name,
  description: props.description ?? '',
  location: props.location ?? '',
  badgeImage: props.badgeImage ?? '',
  startDateTime: dayjs.unix(props.startDateTime).toISOString(),
  endDateTime: dayjs.unix(props.endDateTime).toISOString(),
})

const schema = z.object({
  name: z.string()
    .min(1, 'Please enter a name'),
  description: z.string(),
  location: z.string(),
  badgeImage: z.string()
    .url('Please enter a URL'),
  startDateTime: z.string()
    .refine(val => dayjs(val).isAfter(dayjs()), 'Start date must be in the future')
    .transform(d => dayjs(d).unix().toString()),
  endDateTime: z.string()
    .transform(d => dayjs(d).unix().toString()),
}).refine((val) => {
  return dayjs(val.startDateTime).isBefore(val.endDateTime)
}, {
  message: 'Event must end after it starts',
  path: ['endDateTime'],
})

type Schema = z.output<typeof schema>

const { mutate: updateEventMutate, isPending: updateEventIsPending } = useUpdateEventMutation(props.id)

function updateEvent({ data }: FormSubmitEvent<Schema>) {
  updateEventMutate(data, {
    onError(err) {
      toast.add({
        title: err.message,
      })
    },
  })
}
</script>

<template>
  <UForm :state="state" :schema="schema" class="space-y-7" @submit="updateEvent">
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

    <UButton type="submit" :loading="updateEventIsPending">
      Update
    </UButton>
  </UForm>
</template>
