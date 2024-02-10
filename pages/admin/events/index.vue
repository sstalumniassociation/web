<script setup lang="ts">
import { FilterMatchMode } from 'primevue/api'

const dayjs = useDayjs()

const { data: events, isPending: eventsPending } = useEvents()

const state = reactive({
  createPopupVisible: false,
})

const filters = ref({
  global: { value: null, matchMode: FilterMatchMode.CONTAINS },
})
</script>

<template>
  <div class="px-4 pt-6 space-y-6">
    <div class="flex items-start justify-between">
      <div>
        <h1 class="text-2xl font-semibold mt-0 mb-2">
          Events
        </h1>
        <span class="opacity-80">
          These are the events going on!
        </span>
      </div>
      <Button label="Create" @click="state.createPopupVisible = true">
        <template #icon>
          <span class="i-heroicons-plus-circle mr-2" />
        </template>
      </Button>
    </div>

    <AdminEventCreatePopup v-model:visible="state.createPopupVisible" />

    <div class="ml-auto">
      <InputText v-model="filters.global.value" placeholder="Search" />
    </div>

    <DataTable
      v-model:filters="filters" :value="events" paginator data-key="id" :rows="40"
      :loading="eventsPending" :global-filter-fields="['name']"
    >
      <Column field="name" header="Name" class="min-w-sm">
        <template #body="{ data }">
          <NuxtLink :to="`/admin/events/${data.id}`">
            <Button link :label="data.name" class="text-left font-semibold p-0" />
          </NuxtLink>
        </template>
      </Column>

      <Column field="dateTime" header="Date" class="min-w-sm">
        <template #body="{ data }">
          <Badge>
            {{ dayjs.unix(data.startDateTime).format('lll') }}
          </Badge>
          -
          <Badge>
            {{ dayjs.unix(data.endDateTime).format('lll') }}
          </Badge>
        </template>
      </Column>

      <Column field="attendees" header="Attendees">
        <template #body="{ data }">
          {{ data.attendees.length }}
        </template>
      </Column>
    </DataTable>
  </div>
</template>
