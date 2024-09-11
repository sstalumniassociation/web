<script setup lang="ts">
import { FilterMatchMode } from '@primevue/core'

const dayjs = useDayjs()
const { data, isPending: checkInsPending } = useCheckInsWithAdminScope()

const sizeOptions: { label: string, value: 'small' | 'large' | undefined }[] = [
  { label: 'Small', value: 'small' },
  { label: 'Normal', value: undefined },
  { label: 'Large', value: 'large' },
]

const size = ref(sizeOptions[1])

const filters = ref({
  global: { value: null, matchMode: FilterMatchMode.CONTAINS },
})
</script>

<template>
  <div class="p-4 space-y-4">
    <div class="flex md:justify-between md:items-center flex-col md:flex-row flex-gap-3">
      <span class="text-2xl font-semibold">
        Check ins
      </span>

      <div class="flex flex-col md:flex-row items-start flex-gap-3">
        <SelectButton v-model="size" :options="sizeOptions" option-label="label" data-key="label" />
        <InputText v-model="filters.global.value" placeholder="Search" />
      </div>
    </div>

    <DataTable
      v-model:filters="filters" removable-sort paginator data-key="id" :rows="40" :value="data?.checkIns"
      :global-filter-fields="['user.name', 'user.phone', 'guest.name', 'guest.phone']" :size="size.value" :loading="checkInsPending"
    >
      <template #empty>
        No check ins found.
      </template>

      <Column header="Check In">
        <template #body="{ data }">
          {{ dayjs(data.checkInDateTime).format("DD/MM/YYYY HH:mm") }}
        </template>
      </Column>

      <Column header="Type">
        <template #body="{ data }">
          <Tag v-if="data.user" value="User" />
          <Tag v-else-if="data.guest" value="Guest" />
        </template>
      </Column>

      <Column header="Checked out">
        <template #body="{ data }">
          <template v-if="data.checkOutDateTime">
            {{ dayjs(data.checkOutDateTime).format("DD/MM/YYYY HH:mm") }}
          </template>
          <Tag v-else value="No" severity="warn" />
        </template>
      </Column>

      <Column header="Name" sortable>
        <template #body="{ data }">
          <span v-if="data.user">{{ data.user.name }}</span>
          <span v-else-if="data.guest">{{ data.guest.name }}</span>
        </template>
      </Column>

      <Column header="Phone" sortable>
        <template #body="{ data }">
          <Tag v-if="data.user" value="N/A" />
          <span v-else-if="data.guest">{{ data.guest.phone }}</span>
        </template>
      </Column>
    </DataTable>
  </div>
</template>
