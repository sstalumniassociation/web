<script setup lang="ts">
import type { EventWithAttendees } from '~/shared/types'

const props = defineProps<{
  event: EventWithAttendees
  showPopup: boolean
}>()

// const mutation = useMutation({
//     mutationFn: (id: string) => $api(`/api/event/${id}` as '/api/event/:id', {
//         method: 'PUT',
//         body: updatedEvent,
//     }),
// })

// const handleSubmit = async (event: FormSubmitEvent<typeof state>) => {
//     mutation.mutate(props.event.id)
// }

const closePopup = defineEmits(['closePopup'])

const state = reactive({

})
</script>

<template>
  <div>
    <UModal v-model="props.showPopup">
      <UCard>
        <template #header>
          <div class="flex flex-row items-start">
            <div>
              <h1 class="text-xl font-bold pb-1">
                Update Event
              </h1>
              <p class="text-md">
                Update information regarding {{ props.event.name }}. Note that Event ID cannot be changed.
              </p>
            </div>
            <UButton color="gray" variant="ghost" icon="i-heroicons-x-mark-20-solid" class="ml-auto -my-1" @click="$emit('closePopup')" />
          </div>
        </template>

        <UForm :state="state" class="space-y-2" />
      </UCard>
    </UModal>
  </div>
</template>
