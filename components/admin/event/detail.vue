<script setup lang="ts">
import { f7NavTitle, f7Navbar, f7Page } from 'framework7-vue'
import type { Router } from 'framework7/types'
import { typeIsEventWithAttendees } from '~/composables/event'

const props = defineProps<{
  f7route: Router.Route
}>()
const { data: event } = useEvent(props.f7route.params.id!)
</script>

<template>
  <f7Page>
    <f7Navbar back-link="Back">
      <f7NavTitle>
        Event Details
      </f7NavTitle>
    </f7Navbar>

    <AdminEventInformation v-if="typeIsEventWithAttendees(event)" :event="event" />

    <AdminEventUploadAttendees v-if="typeIsEventWithAttendees(event)" :event="event" />

    <AdminEventCheckedinUsers v-if="typeIsEventWithAttendees(event)" :event="event" />
  </f7Page>
</template>
