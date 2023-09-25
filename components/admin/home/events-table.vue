<script setup lang="ts">
import dayjs from 'dayjs'
import { f7, f7Button, f7Card, f7CardContent, f7CardFooter, f7CardHeader, f7Chip } from 'framework7-vue'
import { EventWithAttendees } from '~/shared/types';

const { data: events } = useEvents()

const state = reactive({
  eventId: "",
  eventName: ""
})

function prepareProps(event: EventWithAttendees) {
  state.eventId = event.id
  state.eventName = event.name
}

function showDetails(event: EventWithAttendees) {
  f7.views.main.router.navigate(`/admin/event/${ event.id }`, {
      transition: "f7-push"
  })
}

</script>

<template>
  <div>
    <div class="md:hidden">
      <f7Card v-for="event in events" :key="event.id">
        <f7CardHeader>
          <div class="flex flex-col items-start space-y-4">
            <f7Chip outline :text="`0 / ${event.attendees !== undefined ? event.attendees.length : ''}`" />
            <span>
              {{ event.name }}
            </span>
          </div>
        </f7CardHeader>
        <f7CardContent>
          {{ event.description }}
        </f7CardContent>
        <f7CardFooter>
          <f7Button tonal @click="showDetails(event)">
            Open
          </f7Button>
          <f7Button tonal color="red" @click="prepareProps(event)" class="popup-open" data-popup=".delete-event-popup">
            Delete
          </f7Button>
        </f7CardFooter>
      </f7Card>
    </div>

    <div class="hidden md:inline">
      <div class="data-table data-table-init card">
        <div class="card-header">
          <div class="data-table-title">Events</div>
          <div></div>
          <div class="data-table-actions">
            <a class="button popup-open icon-only" data-popup=".create-event-popup">
              <i class="icon f7-icons"> plus_circle </i>
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
                <f7Button tonal color="red" class="popup-open" @click="prepareProps(event)" data-popup=".delete-event-popup">
                  Delete
                </f7Button>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </div>

  <AdminHomeDeleteEventPopup :key="state.eventId" :eventId="state.eventId" :eventName="state.eventName" />
  <AdminHomeCreateEventPopup />
</template>
