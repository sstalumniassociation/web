<script setup lang="ts">
import type { EventWithAttendees } from '~/shared/types'

const props = defineProps<{
  event: EventWithAttendees
}>()
</script>

<template>
  <UCard>
    <template #header>
      <span class="font-semibold text-2xl">Attendees</span>
    </template>
    <div v-for="attendee in props.event.attendees" v-if="props.event.attendees.length > 0" :key="attendee.id">
      <div class="mb-3">
        <p class="font-bold text-xl">
          {{ attendee.name }}
        </p>
        <p><span class="font-semibold">ID: </span>{{ attendee.id }}</p>
        <hr class="my-2">
      </div>
    </div>
    <div v-if="props.event.attendees.length === 0" class="flex justify-center align-items-center">
      <div class="text-center">
        <UIcon class="text-5xl" name="i-heroicons-face-frown" />
        <p class="font-bold text-xl">
          No attendees yet!
        </p>
        <p>Upload attendees using a .csv file first!</p>
      </div>
    </div>
  </UCard>
</template>
