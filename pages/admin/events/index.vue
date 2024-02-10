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
    <div class="flex items-center justify-between">
      <div>
        <h1 class="text-2xl font-semibold">
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

    <div class="flex flex-gap-3 flex-col md:flex-row md:items-center">
      <InputText v-model="filters.global.value" placeholder="Keyword Search" />
    </div>

    <DataTable
      v-model:filters="filters" :value="events" paginator data-key="id" :rows="40"
      :loading="eventsPending" :global-filter-fields="['name']"
    >
      <Column field="name" header="Name">
        <template #body="{ data }">
          {{ data.name }}
        </template>
      </Column>

      <Column field="dateTime" header="Date">
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

      <Column field="attendees" header="attendees">
        <template #body="{ data }">
          {{ data.attendees.length }}
        </template>
      </Column>

      <!-- <NuxtLink class="font-semibold hover:underline" variant="link" :to="`/admin/events/${data}`">
          </NuxtLink> -->
    </DataTable>
  </div>
</template>
