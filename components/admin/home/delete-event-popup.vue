<script setup lang="ts">
import type { FormError } from '@nuxt/ui/dist/runtime/types'
import { useMutation, useQueryClient } from '@tanstack/vue-query'
import type { EventWithAttendees } from '~/shared/types'

const props = defineProps<{
  event: EventWithAttendees
  showPopup: boolean
}>()
const queryClient = useQueryClient()

const closePopup = defineEmits(['closePopup'])

const state = reactive({
  verification: '',
  deleteStatus: false,
})

function validate(values: typeof state): FormError[] {
  const error: FormError[] = []
  if (values.verification !== props.event.name)
    error.push({ path: 'verification', message: 'Event ID does not match!' })
  return error
}

const mutation = useMutation({
  mutationFn: (id: string) => $api(`/api/event/${id}` as '/api/event/:id', {
    method: 'DELETE',
  }),
})

async function handleSubmit() {
  mutation.mutate(props.event.id)
  state.deleteStatus = true
  queryClient.invalidateQueries({ queryKey: ['events'] })
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
                Delete Event
              </h1>
              <p class="text-md">
                This event will be <span class="font-bold">permanently deleted</span>, including the record of the attendees who attended the event.
              </p>
            </div>
            <UButton color="gray" variant="ghost" icon="i-heroicons-x-mark-20-solid" class="ml-auto -my-1" @click="$emit('closePopup')" />
          </div>
        </template>

        <UForm :validate="validate" :state="state" class="space-y-2" @submit="handleSubmit">
          <p>To verify, type the <span class="font-bold">Event Name {{ `(${props.event.name})` }}</span> in the field below:</p>
          <div class="space-y-5">
            <UFormGroup name="verification">
              <UInput v-model="state.verification" color="gray" variant="outline" :placeholder="props.event.name" />
            </UFormGroup>

            <UButton v-if="!state.deleteStatus" type="submit">
              Delete Event
            </UButton>
            <UButton v-if="state.deleteStatus" color="green" @click="$emit('closePopup')">
              Delete Successful! Close
            </UButton>
          </div>
        </UForm>
      </UCard>
    </UModal>
  </div>
</template>
