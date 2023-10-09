<script setup lang="ts">
import 'tailwindcss/src/css/preflight.css'
import { typeIsEventWithAttendees } from '~/composables/event'

const route = useRoute()

const { data: event } = useEvent(route.params.id)
</script>

<template>
  <div class="w-full">
    NavBar goes here
  </div>
  <UContainer class="py-3 space-y-7">
    <AdminEventInformation v-if="typeIsEventWithAttendees(event)" :event="event" />
    <AdminEventUploadAttendees v-if="typeIsEventWithAttendees(event)" :event="event" />
    <AdminEventCheckedinUsers v-if="typeIsEventWithAttendees(event)" :event="event" />
  </UContainer>
</template>
