<script setup lang="ts">
import { useForm } from 'vee-validate'
import { toTypedSchema } from '@vee-validate/zod'
import { z } from 'zod'

const props = defineProps<{
  id?: string
  name?: string
  description?: string
  location?: string
  badgeImage?: string
  startDateTime?: string
  endDateTime?: string
}>()

if (!props.id) {
  throw new Error('Event ID is required')
}

const toast = useToast()
const dayjs = useDayjs()

const schema = toTypedSchema(
  z.object({
    name: z.string().min(1, 'Please enter a name'),
    description: z.string(),
    location: z.string(),
    badgeImage: z.string().url('Please enter a URL'),
    startDateTime: z.date(),
    endDateTime: z.date(),
  }).refine((val) => {
    return dayjs(val.startDateTime).isBefore(val.endDateTime)
  }, {
    message: 'Event must end after it starts',
    path: ['endDateTime'],
  }),
)

const { defineField, handleSubmit, errors, resetForm } = useForm({
  validationSchema: schema,
  initialValues: {
    name: props.name,
    description: props.description ?? '',
    location: props.location ?? '',
    badgeImage: props.badgeImage ?? '',
    startDateTime: dayjs(props.startDateTime).toDate(),
    endDateTime: dayjs(props.endDateTime).toDate(),
  },
})

const [formName, nameProps] = defineField('name')
const [formDescription, descriptionProps] = defineField('description')
const [formLocation, locationProps] = defineField('location')
const [formBadgeImage, badgeImageProps] = defineField('badgeImage')
const [formStartDateTime] = defineField('startDateTime')
const [formEndDateTime] = defineField('endDateTime')

const { mutate: updateEventMutate, isPending: updateEventIsPending } = useUpdateEventMutation(props.id)

const updateEvent = handleSubmit((values) => {
  const data = {
    event: {
      ...values,
      startDateTime: values.startDateTime.toString(),
      endDateTime: values.startDateTime.toString(),
    },
  }

  updateEventMutate(data, {
    onError(err) {
      toast.add({
        summary: err.message,
      })
    },
  })
})
</script>

<template>
  <form class="flex flex-col space-y-3" @submit="updateEvent">
    <InputText v-model="formName" v-bind="nameProps" placeholder="Name (example: SST Homecoming)" class="w-full" />
    <small class="p-error">{{ errors.name }}</small>

    <Textarea
      v-model="formDescription" v-bind="descriptionProps" rows="5"
      placeholder="Description (example: Come back to 1 Technology Drive for our inaugural homecoming!)"
      class="w-full resize-y"
    />
    <small class="p-error">{{ errors.description }}</small>

    <span class="font-semibold pt-2">Event start</span>

    <div class="flex flex-gap-3">
      <Calendar v-model="formStartDateTime" class="w-3/4" placeholder="Date" />
      <Calendar v-model="formStartDateTime" time-only show-time hour-format="12" placeholder="Time" />
    </div>
    <small class="p-error">{{ errors.startDateTime }}</small>

    <span class="font-semibold pt-2">Event end</span>

    <div class="flex flex-gap-3">
      <Calendar v-model="formEndDateTime" class="w-3/4" placeholder="End date" />
      <Calendar v-model="formEndDateTime" time-only show-time hour-format="12" placeholder="End time" />
    </div>
    <small class="p-error">{{ errors.endDateTime }}</small>

    <span class="font-semibold pt-2">Miscellaneous</span>

    <InputText
      v-model="formLocation" v-bind="locationProps"
      placeholder="Location (example: 1 Technology Drive, Singapore)" class="w-full"
    />
    <small class="p-error">{{ errors.location }}</small>

    <InputText
      v-model="formBadgeImage" v-bind="badgeImageProps" type="url"
      placeholder="Badge URL (example: https://app.sstaa.org/cdn/logo.png)" class="w-full"
    />
    <small class="p-error">{{ errors.badgeImage }}</small>

    <div class="flex flex-gap-2 mt-3">
      <Button type="submit" :loading="updateEventIsPending">
        Update
      </Button>

      <Button outlined @click="() => resetForm()">
        Reset
      </Button>
    </div>
  </form>
</template>
