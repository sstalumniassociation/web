<script setup lang="ts">
import 'tailwindcss/src/css/preflight.css'
import { typeIsEventWithAttendees } from '~/composables/event'

const route = useRoute()

const { data: event } = useEvent(route.params.id)
</script>

<template>
  <div class="w-full py-3 border-b-2 border-gray-800">
    <div class="pl-6 flex flex-row items-center content-center">
        <UButton color="gray" icon="i-heroicons-arrow-small-left" variant="ghost" @click="$router.go(-1)" />
        <p class="text-2xl font-semibold ml-auto mr-auto">Event Detail</p>
    </div>
  </div>
  <UContainer class="py-3 space-y-7">
    <AdminEventInformation v-if="typeIsEventWithAttendees(event)" :event="event" />
    <AdminEventUploadAttendees v-if="typeIsEventWithAttendees(event)" :event="event" />
    <AdminEventCheckedinUsers v-if="typeIsEventWithAttendees(event)" :event="event" />
  </UContainer>
</template>
