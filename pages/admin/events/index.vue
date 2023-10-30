<script setup lang="ts">
const columns = [
  {
    key: 'name',
    label: 'Name',
  },
  {
    key: 'startDateTime',
    label: 'Start',
  },
  {
    key: 'endDateTime',
    label: 'End',
  },
  {
    key: 'participants',
    label: 'Participants',
  },
]

const { data: events, isLoading: eventsIsLoading, error: eventsError } = useEvents()

const state = reactive({
  createPopupVisible: false,
})
</script>

<template>
  <div class="px-4 pt-6 space-y-6">
    <div class="flex items-center justify-between">
      <div>
        <h1 class="text-2xl font-semibold">
          Events
        </h1>
        <span class="opacity-80">
          These are the events going on!
        </span>
      </div>
      <UButton icon="i-heroicons-plus-circle" @click="state.createPopupVisible = true">
        Create
      </UButton>
    </div>

    <AdminEventCreatePopup v-model:visible="state.createPopupVisible" />

    <UTable
      :rows="events" :columns="columns"
      :empty-state="{ icon: 'i-heroicons-circle-stack-20-solid', label: 'No Events' }"
    >
      <template #name-data="{ row }">
        <NuxtLink class="font-semibold hover:underline" variant="link" :to="`/admin/events/${row.id}`">
          {{ row.name }}
        </NuxtLink>
      </template>
      <template #startDateTime-data="{ row }">
        {{ $dayjs.unix(row.startDateTime).format('DD MMM YYYY') }}
      </template>
      <template #endDateTime-data="{ row }">
        {{ $dayjs.unix(row.endDateTime).format('DD MMM YYYY') }}
      </template>
      <template #participants-data="{ row }">
        <UBadge>
          {{ row.attendees.length }}
        </UBadge>
      </template>
    </UTable>
  </div>
</template>
