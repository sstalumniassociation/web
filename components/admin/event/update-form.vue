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
  <form class="flex flex-col space-y-3" @submit.prevent="updateEvent">
    <InputText v-model="state.name" autofocus placeholder="Name (example: SST Homecoming)" class="w-full" />

    <Textarea
      v-model="state.description" rows="5"
      placeholder="Description (example: Come back to 1 Technology Drive for our inaugural homecoming!)"
      class="w-full resize-y"
    />

    <span class="font-semibold pt-2">Event start</span>

    <div class="flex flex-gap-3">
      <Calendar v-model="state.startDateTime" class="w-3/4" placeholder="Date" />
      <Calendar v-model="state.startDateTime" time-only show-time hour-format="12" placeholder="Time" />
    </div>

    <span class="font-semibold pt-2">Event end</span>

    <div class="flex flex-gap-3">
      <Calendar v-model="state.endDateTime" class="w-3/4" placeholder="End date" />
      <Calendar v-model="state.endDateTime" time-only show-time hour-format="12" placeholder="End time" />
    </div>

    <span class="font-semibold pt-2">Miscellaneous</span>

    <InputText
      v-model="state.location" placeholder="Location (example: 1 Technology Drive, Singapore)"
      class="w-full"
    />
    <InputText
      v-model="state.badgeImage" type="url"
      placeholder="Badge URL (example: https://app.sstaa.org/cdn/logo.png)" class="w-full"
    />

    <div>
      <Button type="submit" :loading="updateEventIsPending" class="mt-3">
        Update
      </Button>
    </div>
  </form>
</template>
