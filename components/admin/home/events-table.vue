<script setup lang="ts">
import dayjs from 'dayjs'
import { f7Button, f7Card, f7CardContent, f7CardFooter, f7CardHeader, f7Chip } from 'framework7-vue'

const { data: events } = useEvents()
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
          <f7Button tonal>
            Open
          </f7Button>
        </f7CardFooter>
      </f7Card>
    </div>

    <div class="hidden md:inline">
      <div class="data-table data-table-init card">
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
              <td class="label-cell">
                {{ event.name }}
              </td>
              <td class="label-cell">
                {{ dayjs(event.startDateTime).format('DD-MM-YYYY hh:mm') }}
              </td>
              <td class="label-cell">
                {{ dayjs(event.endDateTime).format('DD-MM-YYYY hh:mm') }}
              </td>
              <td class="numeric-cell">
                {{ event.attendees.length }}
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </div>
</template>
