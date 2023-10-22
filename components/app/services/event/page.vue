<script setup lang="ts">
import { f7BlockTitle, f7Card, f7Chip, f7Link, f7List, f7ListItem, f7NavRight, f7NavTitle, f7Navbar, f7Page, f7PageContent, f7Searchbar, f7SkeletonBlock, f7Subnavbar } from 'framework7-vue'

import type { VirtualList } from 'framework7/types'
import type { UnwrapRef } from 'vue'

import { useDatabaseList } from 'vuefire'
import { ref as dbRef } from 'firebase/database'

const props = defineProps<{
  id: string // Passed by f7router in URL param
}>()

const { $dayjs } = useNuxtApp()

const db = useDatabase()
const { data: checkedInUsersList, pending: checkedInUsersListPending } = useDatabaseList<{ $value: number }>(dbRef(db, props.id))
const { data: event, isLoading: eventIsLoading } = useEvent(props.id)

const attendees = computed(() => {
  return event.value?.attendees.map((attendee, index) => ({
    ...attendee,
    index, // Used by f7 virtual list
  })) ?? []
})

const checkedInUsers = computed(() => {
  const data: Record<string, number> = {} // admissionKey -> timestamp in seconds
  for (const item of checkedInUsersList.value)
    data[item.id] = item.$value
  return data
})

const attendeeVirtualListData = ref<Omit<Partial<VirtualList.VirtualListRenderData>, 'items'> & { items: UnwrapRef<typeof attendees> }>({
  items: [],
})

const formattedDateCache = new Map<number, string>()

function formattedDate(date: number) {
  if (formattedDateCache.has(date))
    return formattedDateCache.get(date)

  const data = $dayjs(date * 1000).fromNow()
  formattedDateCache.set(date, data)
  return data
}

function searchAll(query: string, items: UnwrapRef<typeof attendees>) {
  const found = []
  for (const idx in items) {
    if (items[idx].name.toLowerCase().includes(query.toLowerCase()) || query.trim() === '')
      found.push(idx)
  }
  return found
}

function renderExternal(_: unknown, data: VirtualList.VirtualListRenderData) {
  attendeeVirtualListData.value = data
}
</script>

<template>
  <f7Page>
    <f7Navbar>
      <f7NavTitle>SSTAARS</f7NavTitle>
      <f7NavRight>
        <f7Link popup-close>
          Close
        </f7Link>
      </f7NavRight>
      <f7Subnavbar :inner="false">
        <f7Searchbar search-container=".virtual-list" search-item="li" search-in=".item-title" />
      </f7Subnavbar>
    </f7Navbar>

    <f7List v-if="eventIsLoading || checkedInUsersListPending" inset>
      <div class="space-y-2">
        <f7SkeletonBlock v-for="n in 2" :key="n" :height="`${n * 100}px`" class="rounded-md" effect="fade" />
      </div>
    </f7List>

    <template v-else-if="event">
      <f7BlockTitle large>
        {{ event.name }}
      </f7BlockTitle>

      <f7Card>
        <template #header>
          <div class="flex justify-between w-full">
            <div>
              Checked in
            </div>
            <div class="rounded-md">
              {{ checkedInUsersList.length }} / {{ event.attendees.length }}
            </div>
          </div>
        </template>
      </f7Card>

      <f7BlockTitle>Attendees</f7BlockTitle>

      <f7List
        strong inset virtual-list :virtual-list-params="{
          items: attendees,
          searchAll,
          renderExternal,
          height: 50,
        }"
      >
        <ul>
          <f7ListItem
            v-for="attendee in attendeeVirtualListData.items" :key="attendee.admissionKey"
            :style="`top: ${attendeeVirtualListData.topPosition}px`" :virtual-list-index="attendee.index" :title="attendee.name"
          >
            <f7Chip v-if="checkedInUsers[attendee.admissionKey]" color="green">
              <span class="text-xs!">
                {{ formattedDate(checkedInUsers[attendee.admissionKey]) }}
              </span>
            </f7Chip>
            <f7Chip v-else color="red">
              Not checked in
            </f7Chip>
          </f7ListItem>
        </ul>
      </f7List>
    </template>
  </f7Page>
</template>
