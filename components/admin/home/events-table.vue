<script setup lang="ts">
import dayjs from 'dayjs'

const { data: events } = useEvents()
</script>

<template>
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
</template>
