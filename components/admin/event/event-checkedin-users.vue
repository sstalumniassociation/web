<script setup lang="ts">
import { f7Card, f7CardContent, f7CardHeader, f7List, f7ListItem } from 'framework7-vue'
import type { EventWithAttendees } from '~/shared/types'

const props = defineProps<{
  event: EventWithAttendees
}>()
</script>

<template>
  <f7Card>
    <f7CardHeader>
      <span class="font-semibold text-2xl">Attendees</span>
    </f7CardHeader>
    <f7CardContent>
      <f7List v-for="attendee in props.event.attendees" :key="attendee.id" media-list dividers-ios strong inset>
        <f7ListItem :title="attendee.name" :subtitle="`ID: ${attendee.id}`" />
      </f7List>
    </f7CardContent>
  </f7Card>
</template>
