<script setup lang="ts">
import dayjs from 'dayjs'
import { ref } from 'vue'
import type { EventWithAttendees } from '~/shared/types'

const { data: events } = useEvents()

// States
const showDeletePopup = ref(false)
const showEditPopup = ref(false)
const showCreatePopup = ref(false)
const selectedEvent = ref<Partial<EventWithAttendees>>({})

const columns = [{
  key: 'name',
  label: 'Name',
}, {
  key: 'startDateTime',
  label: 'Start Date',
}, {
  key: 'endDateTime',
  label: 'End Date',
}, {
  key: 'participants',
  label: 'Participants',
}, {
  key: 'actions',
}]

function editActions(row: EventWithAttendees) {
  return [
    [{
      label: 'Edit',
      icon: 'i-heroicons-pencil-square-20-solid',
      click: () => {
        selectedEvent.value = row
        toggleEditPopup()
      },
    }, {
      label: 'Delete',
      icon: 'i-heroicons-trash',
      click: () => {
        selectedEvent.value = row
        toggleDeletePopup()
      },
    }],
  ]
}

// Toggle Popups
function toggleCreatePopup() {
  showCreatePopup.value = !showCreatePopup.value
}

function toggleDeletePopup() {
  showDeletePopup.value = !showDeletePopup.value
}

function toggleEditPopup() {
  showEditPopup.value = !showEditPopup.value
}
</script>

<template>
  <div class="rounded-xl border border-slate p-5 space-y-4">
    <div class="flex flex-row">
      <h1 class="text-3xl font-bold">
        Events
      </h1>
      <UButton icon="i-heroicons-plus-circle" label="Create" class="ml-auto px-4" color="blue" @click="toggleCreatePopup" />
    </div>
    <UTable class="" :rows="events" :columns="columns" :empty-state="{ icon: 'i-heroicons-circle-stack-20-solid', label: 'No Events' }">
      <template #name-data="{ row }">
        <span class="w-full"><NuxtLink :to="`/admin/event/${row.id}`">{{ row.name }}</NuxtLink></span>
      </template>
      <template #startDateTime-data="{ row }">
        <span class="w-full"><NuxtLink :to="`/admin/event/${row.id}`">{{ dayjs(parseInt(row.startDateTime) * 1000).format('DD-MM-YYYY HH:mm') }}</NuxtLink></span>
      </template>
      <template #endDateTime-data="{ row }">
        <span class="w-full"><NuxtLink :to="`/admin/event/${row.id}`">{{ dayjs(parseInt(row.endDateTime) * 1000).format('DD-MM-YYYY HH:mm') }}</NuxtLink></span>
      </template>
      <template #participants-data="{ row }">
        <span class="w-full"><NuxtLink :to="`/admin/event/${row.id}`">{{ row.attendees.length }}</NuxtLink></span>
      </template>
      <template #actions-data="{ row }">
        <UDropdown :items="editActions(row)">
          <UButton color="gray" variant="ghost" icon="i-heroicons-ellipsis-horizontal-20-solid" />
        </UDropdown>
      </template>
    </UTable>
  </div>

  <AdminHomeCreateEventPopup :show-popup="showCreatePopup" @close-popup="toggleCreatePopup" />
  <AdminHomeDeleteEventPopup :event="selectedEvent as EventWithAttendees" :show-popup="showDeletePopup" :key="selectedEvent.id" @close-popup="toggleDeletePopup" />
  <AdminHomeUpdateEventPopup :show-popup="showEditPopup" :event="selectedEvent as EventWithAttendees" :key="selectedEvent.id" @close-popup="toggleEditPopup" />
</template>
