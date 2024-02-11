<script setup lang="ts">
import { FilterMatchMode } from 'primevue/api'

const { data: users, isPending: usersPending } = useUsers()

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
        Members
      </span>

      <div class="flex flex-col md:flex-row items-start flex-gap-3">
        <SelectButton v-model="size" :options="sizeOptions" option-label="label" data-key="label" />
        <InputText v-model="filters.global.value" placeholder="Search" />
      </div>
    </div>

    <DataTable
      v-model:filters="filters"
      removable-sort paginator data-key="id" :rows="40" :value="users" :size="size.value"
      :loading="usersPending" :global-filter-fields="['name', 'email']"
    >
      <template #empty>
        No members found.
      </template>

      <Column field="id" header="ID" class="min-w-xs w-[10%]" />
      <Column field="name" header="Name" sortable class="min-w-sm w-[40%]" />
      <Column field="email" header="Email" sortable class="min-w-smw-[40%]" />
      <Column field="graduationYear" header="Graduation Year" sortable class="min-w-xs w-[10%]" />
    </DataTable>
  </div>
</template>
