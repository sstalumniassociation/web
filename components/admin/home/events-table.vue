<script setup lang="ts">
import dayjs from 'dayjs';
import { typeIsEventWithAttendees } from '~/composables/event'
const { data: events } = useEvents()

interface EventRow {
  id: string,
  name: string,
  startDateTime: string,
  endDateTime: string,
  participants: number,
}

let formattedEvents: EventRow[] = []

if (events.value !== undefined) {
  for (let i = 0; i < events.value?.length; i++) {
    formattedEvents.push({
      id: events.value[i].id,
      name: events.value[i].name,
      startDateTime: dayjs(parseInt(events.value[i].startDateTime) * 1000).format('DD-MM-YYYY HH:mm'),
      endDateTime: dayjs(parseInt(events.value[i].endDateTime) * 1000).format('DD-MM-YYYY HH:mm'),
      participants: events.value[i].attendees.length,
    })
  }
}

const columns = [{
  key: 'name',
  label: 'Name'
}, {
  key: 'startDateTime',
  label: 'Start Date'
}, {
  key: 'endDateTime',
  label: 'End Date'
}, {
  key: 'participants',
  label: 'Participants'
}, {
  key: "actions"
}]

const editActions = (row: EventRow) => [
  [{
    label: 'Edit',
    icon: 'i-heroicons-pencil-square-20-solid',
    click: () => console.log('Edit', row.id)
  }, {
    label: 'Delete',
    icon: 'i-heroicons-trash',
    click: () => console.log("Delete", row.id)
  }]
]

</script>

<template>
  <div class="p-6">
    <UTable :rows='formattedEvents' :columns="columns" :empty-state="{ icon: 'i-heroicons-circle-stack-20-solid', label: 'No Events' }">
      <template #actions-data="{ row }">
        <UDropdown :items="editActions(row)">
          <UButton color="gray" variant="ghost" icon="i-heroicons-ellipsis-horizontal-20-solid" />
        </UDropdown>
      </template>
    </UTable>
  </div>
</template>