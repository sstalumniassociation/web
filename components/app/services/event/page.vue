<script setup lang="ts">
import { f7 } from 'framework7-vue'

import type { VirtualList } from 'framework7/types'
import type { UnwrapRef } from 'vue'

import { useDatabaseList } from 'vuefire'
import { ref as dbRef, remove, set } from 'firebase/database'

const props = defineProps<{
  id: string // Passed by f7router in URL param
}>()

const state = ref({
  scannerOpened: false,
})

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

  const data = $dayjs.unix(date).format('hh:mm A')
  formattedDateCache.set(date, data)
  return data
}

async function toggle(admissionKey: string) {
  if (checkedInUsers.value[admissionKey])
    await remove(dbRef(db, `${props.id}/${admissionKey}`))
  else
    await set(dbRef(db, `${props.id}/${admissionKey}`), $dayjs().unix())
}

async function onScan(admissionKey: string) {
  const attendee = attendees.value.find(attendee => attendee.admissionKey === admissionKey)
  if (!attendee) // Attendee is in master list from API
    return

  if (checkedInUsers.value[admissionKey]) { // Attendee is already checked in
    f7.toast.show({
      text: `${attendee.name} already checked in`,
      closeTimeout: 3000,
    })
    return
  }

  await set(dbRef(db, `${props.id}/${admissionKey}`), $dayjs().unix())

  f7.toast.show({
    text: `Checked in ${attendee.name}`,
    closeTimeout: 10000,
  })
}

// Virtualized list helpers

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
  <F7Page>
    <F7Navbar>
      <F7NavTitle>SSTAARS</F7NavTitle>
      <F7NavRight>
        <F7Link popup-close>
          Close
        </F7Link>
      </F7NavRight>
      <F7Subnavbar :inner="false">
        <F7Searchbar search-container=".virtual-list" search-item="li" search-in=".item-title" />
      </F7Subnavbar>
    </F7Navbar>

    <F7List v-if="eventIsLoading || checkedInUsersListPending" inset>
      <div class="space-y-2">
        <F7SkeletonBlock v-for="n in 2" :key="n" :height="`${n * 100}px`" class="rounded-md" effect="fade" />
      </div>
    </F7List>

    <template v-else-if="event">
      <F7BlockTitle large>
        {{ event.name }}
      </F7BlockTitle>

      <F7Card>
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
      </F7Card>

      <F7List inset>
        <F7Button tonal @click="state.scannerOpened = !state.scannerOpened">
          {{ state.scannerOpened ? 'Close' : 'Open' }} scanner
        </F7Button>
      </F7List>

      <div v-if="state.scannerOpened">
        <AppServicesEventScanner @scan="onScan" />
      </div>

      <F7BlockTitle>Attendees</F7BlockTitle>

      <F7List
        strong inset virtual-list :virtual-list-params="{
          items: attendees,
          searchAll,
          renderExternal,
          height: 52,
        }"
      >
        <ul>
          <F7ListItem
            v-for="attendee in attendeeVirtualListData.items" :key="attendee.admissionKey" swipeout
            :style="`top: ${attendeeVirtualListData.topPosition}px`" :virtual-list-index="attendee.index"
          >
            <F7SwipeoutActions right>
              <F7SwipeoutButton
                confirm-text="Are you sure?"
                :color="checkedInUsers[attendee.admissionKey] ? 'red' : 'green'" @click="toggle(attendee.admissionKey)"
              >
                {{ checkedInUsers[attendee.admissionKey] ? 'Check out' : 'Check in' }}
              </F7SwipeoutButton>
            </F7SwipeoutActions>

            <template #title>
              {{ attendee.name }}
            </template>

            <template #after>
              <F7Chip v-if="checkedInUsers[attendee.admissionKey]" color="green">
                {{ formattedDate(checkedInUsers[attendee.admissionKey]) }}
              </F7Chip>
              <F7Chip v-else color="red">
                Not checked in
              </F7Chip>
            </template>
          </F7ListItem>
        </ul>
      </F7List>
    </template>
  </F7Page>
</template>
