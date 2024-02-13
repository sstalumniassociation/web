<script setup lang="ts">
import { z } from 'zod'

const visible = defineModel<boolean>('visible')
const dayjs = useDayjs()
const toast = useToast()

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
  return Number.parseInt(val.startDateTime) < Number.parseInt(val.endDateTime)
}, {
  message: 'Event must end after it starts',
  path: ['endDateTime'],
})

const state = reactive({
  name: '',
  description: '',
  location: '',
  badgeImage: '',
  startDateTime: '',
  endDateTime: '',
  errors: {},
})

const { mutate: createEventMutate, isPending: createEventIsPending } = useCreateEventMutation()

async function createEvent() {
  const result = await schema.safeParseAsync(state)
  if (!result.success) {
    state.errors = result.error.formErrors.fieldErrors
    return
  }

  createEventMutate(state, {
    onSuccess() {
      visible.value = false
    },
    onError(err) {
      toast.add({
        summary: err.message,
        severity: 'error',
      })
    },
  })
}
</script>

<template>
  <Dialog v-model:visible="visible" modal class="w-full mx-4 md:w-auto md:min-w-xl">
    <template #header>
      <div class="flex flex-col flex-gap-2">
        <span class="text-xl font-semibold">
          Create event
        </span>
        <span class="text-sm opacity-80">
          Add a new event hosted by SSTAA!
        </span>
      </div>
    </template>

    <form class="flex flex-col space-y-3 mt-2" @submit.prevent="createEvent">
      <InputText v-model="state.name" autofocus placeholder="Name (example: SST Homecoming)" class="w-full" :invalid="state.errors.name" />

      <Textarea
        v-model="state.description"
        rows="5"
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

      <div class="ml-auto pt-4">
        <Button type="submit" :loading="createEventIsPending" label="Create event" />
      </div>
    </form>
  </Dialog>
</template>
