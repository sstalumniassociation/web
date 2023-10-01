<script setup lang="ts">
import dayjs from 'dayjs'
import { f7, f7Button, f7Card, f7CardContent, f7CardFooter, f7CardHeader, f7Chip, f7Icon } from 'framework7-vue'
import { ref } from 'vue'
import type { EventWithAttendees } from '~/shared/types'

const { data: events } = useEvents()

const selectedEvent = ref<EventWithAttendees>({
  id: '',
  name: '',
  description: '',
  location: '',
  badgeImage: '',
  startDateTime: 0,
  endDateTime: 0,
  attendees: [],
})

function showDetails(event: EventWithAttendees) {
  f7.views.main.router.navigate(`/admin/event/${event.id}`, {
    transition: 'f7-push',
  })
}
</script>

<template>
  <div>
    <div class="md:hidden">
      <f7Card v-for="event in events" :key="event.id">
        <f7CardHeader>
          <div class="flex flex-col items-start space-y-4">
            <f7Chip outline :text="`0 / ${event.attendees.length}`" />
            <span>
              {{ event.name }}
            </span>
          </div>
        </f7CardHeader>
        <f7CardContent>
          {{ event.description }}
        </f7CardContent>
        <f7CardFooter>
          <f7Button tonal @click="selectedEvent = event">
            Open
          </f7Button>
          <f7Button tonal color="red" class="popup-open" data-popup=".delete-event-popup" @click="selectedEvent = event">
            Delete
          </f7Button>
        </f7CardFooter>
      </f7Card>
    </div>

    <div class="hidden md:inline">
      <div class="data-table data-table-init card">
        <div class="card-header">
          <div class="data-table-title">
            Events
          </div>
          <div />
          <div class="data-table-actions">
            <a class="button popup-open icon-only" data-popup=".create-event-popup">
              <f7Icon f7="plus_circle" />
            </a>
          </div>
        </div>
        <table>
          <thead>
            <tr>
              <th class="label-cell">
                Name
              </th>
              <th class="label-cell">
                Start date
              </th>
              <th class="label-cell">
                End date
              </th>
              <th class="numeric-label">
                Participants
              </th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="event in events" :key="event.id">
              <td class="label-cell" @click="showDetails(event)">
                {{ event.name }}
              </td>
              <td class="label-cell" @click="showDetails(event)">
                {{ dayjs(event.startDateTime).format('DD-MM-YYYY HH:mm') }}
              </td>
              <td class="label-cell" @click="showDetails(event)">
                {{ dayjs(event.endDateTime).format('DD-MM-YYYY HH:mm') }}
              </td>
              <td class="numeric-cell" @click="showDetails(event)">
                {{ event.attendees.length }}
              </td>
              <td>
                <div class="flex-inline space-x-3">
                  <f7Button tonal class="popup-open" data-popup=".update-event-popup" @click="selectedEvent = event">
                    Update
                  </f7Button>
                  <f7Button tonal color="red" class="popup-open" data-popup=".delete-event-popup" @click="selectedEvent = event">
                    Delete
                  </f7Button>
                </div>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </div>

  <AdminHomeDeleteEventPopup :event="selectedEvent" />
  <AdminHomeUpdateEventPopup :event="selectedEvent" />
  <AdminHomeCreateEventPopup />
</template>
