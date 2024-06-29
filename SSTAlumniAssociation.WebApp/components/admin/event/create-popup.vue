<script setup lang="ts">
import { z } from 'zod'
import { useForm } from 'vee-validate'
import { toTypedSchema } from '@vee-validate/zod'

const visible = defineModel<boolean>('visible')
const dayjs = useDayjs()
const toast = useToast()

const schema = toTypedSchema(z.object({
  name: z.string()
    .min(1, 'Please enter a name'),
  description: z.string(),
  location: z.string(),
  badgeImage: z.string()
    .url('Please enter a URL'),
  startDateTime: z.date()
    .refine(val => dayjs(val).isAfter(dayjs()), 'Start date must be in the future')
    .transform(d => dayjs(d).unix()),
  endDateTime: z.date()
    .transform(d => dayjs(d).unix()),
}).refine((val) => {
  return val.startDateTime < val.endDateTime
}, {
  message: 'Event must end after it starts',
  path: ['endDateTime'],
}))

const { defineField, handleSubmit, errors } = useForm({
  validationSchema: schema,
})

const [name, nameProps] = defineField('name')
const [description, descriptionProps] = defineField('description')
const [location, locationProps] = defineField('location')
const [badgeImage, badgeImageProps] = defineField('badgeImage')
const [startDateTime] = defineField('startDateTime')
const [endDateTime] = defineField('endDateTime')

const { mutate: createEventMutate, isPending: createEventIsPending } = useCreateEventMutation()

const createEvent = handleSubmit((values) => {
  createEventMutate(values, {
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
})
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

    <form class="flex flex-col space-y-3 mt-2" @submit="createEvent">
      <InputText v-model="name" autofocus placeholder="Name (example: SST Homecoming)" class="w-full" v-bind="nameProps" :invalid="!!errors.name" />
      <small class="p-error">{{ errors.name }}</small>

      <Textarea
        v-model="description"
        v-bind="descriptionProps"
        rows="5"
        placeholder="Description (example: Come back to 1 Technology Drive for our inaugural homecoming!)"
        class="w-full resize-y"
        :invalid="!!errors.description"
      />
      <small class="p-error">{{ errors.description }}</small>

      <span class="font-semibold pt-2">Event start</span>

      <div class="flex flex-gap-3">
        <Calendar v-model="startDateTime" class="w-3/4" placeholder="Date" :invalid="!!errors.startDateTime" />
        <Calendar v-model="startDateTime" time-only show-time hour-format="12" placeholder="Time" :invalid="!!errors.startDateTime" />
      </div>
      <small class="p-error">{{ errors.startDateTime }}</small>

      <span class="font-semibold pt-2">Event end</span>

      <div class="flex flex-gap-3">
        <Calendar v-model="endDateTime" class="w-3/4" placeholder="End date" :invalid="!!errors.endDateTime" />
        <Calendar v-model="endDateTime" time-only show-time hour-format="12" placeholder="End time" :invalid="!!errors.endDateTime" />
      </div>
      <small class="p-error">{{ errors.endDateTime }}</small>

      <span class="font-semibold pt-2">Miscellaneous</span>

      <InputText
        v-bind="locationProps"
        v-model="location" placeholder="Location (example: 1 Technology Drive, Singapore)"
        class="w-full"
        :invalid="!!errors.location"
      />
      <small class="p-error">{{ errors.location }}</small>

      <InputText
        v-bind="badgeImageProps"
        v-model="badgeImage" type="url"
        :invalid="!!errors.badgeImage"
        placeholder="Badge URL (example: https://app.sstaa.org/cdn/logo.png)" class="w-full"
      />
      <small class="p-error">{{ errors.badgeImage }}</small>

      <div class="ml-auto pt-4 space-x-2">
        <Button type="submit" :loading="createEventIsPending" label="Create event" />
        <Button type="reset" label="Reset" outlined />
      </div>
    </form>
  </Dialog>
</template>
