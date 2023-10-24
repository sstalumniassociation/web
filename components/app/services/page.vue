<script setup lang="ts">
import { FetchError } from 'ofetch'
import { f7 } from 'framework7-vue'
import type { EventWithAttendees } from '~/shared/types'

const sstaarsStore = useSstaarsStore()

const state = reactive({
  sheetOpened: false,
  eventId: '',
  pending: false,
})

async function click() {
  if (state.eventId.length === 0)
    return

  state.pending = true
  try {
    const data: EventWithAttendees = await $api(`/api/event/${state.eventId}`)
    sstaarsStore.value.previousEvents[data.id] = data.name
    state.sheetOpened = false

    await openEvent(state.eventId)
  }
  catch (err) {
    if (err instanceof FetchError) {
      f7.toast.show({
        text: err.statusMessage,
        closeTimeout: 5000,
      })
    }
  }
  finally {
    state.pending = false
  }
}

async function openEvent(id: string) {
  state.sheetOpened = false
  await nextTick(() => {
    f7.view.current.router.navigate(`/app/services/event/${id}`, { openIn: 'popup' })
  })
}
</script>

<template>
  <F7Page>
    <F7Navbar large transparent :sliding="false">
      <F7NavTitle sliding>
        Alumni Services
      </F7NavTitle>
      <F7NavTitleLarge>
        Alumni Services
      </F7NavTitleLarge>
    </F7Navbar>

    <F7List v-if="$growthbook.isOn('sstaars')" strong inset>
      <F7ListItem class="font-semibold">
        SSTAA Registration System (SSTAARS)
      </F7ListItem>
      <F7ListItem>
        Helping out with an SSTAA event? Access the Event Registration System here.
      </F7ListItem>
      <F7ListButton link="/halp" class="custom-card" @click="state.sheetOpened = true">
        <span>
          Open
        </span>
        <F7Icon material="chevron_right" />
      </F7ListButton>
    </F7List>

    <F7Sheet v-model:opened="state.sheetOpened" style="height: auto" swipe-to-close>
      <F7PageContent>
        <F7BlockTitle large>
          SSTAARS
        </F7BlockTitle>
        <F7Block>
          <p>
            SSTAARS is intended for volunteers helping with SST Alumni Association Event Registration.
          </p>
          <p>
            You will need to have an access code to access SSTAARS.
          </p>
          <p>
            Please reach out to your SSTAA contact if they did not provide you with an access code.
          </p>
        </F7Block>

        <F7List>
          <F7ListInput v-model:value="state.eventId" placeholder="Access code" />
          <div class="m-4">
            <F7Button large fill preloader :loading="state.pending" @click="click">
              Continue
            </F7Button>
          </div>
        </F7List>

        <F7BlockTitle>
          My events
        </F7BlockTitle>
        <F7List inset strong>
          <span v-if="Object.keys(sstaarsStore.previousEvents).length === 0" class="opacity-80">
            No previous events.
          </span>

          <F7ListItem v-for="name, id in sstaarsStore.previousEvents" :key="id" :title="name" @click="openEvent(id)" />
        </F7List>
      </F7PageContent>
    </F7Sheet>
  </F7Page>
</template>

<style scoped>
:deep(.custom-card > .list-button) {
  display: flex !important;
  justify-content: space-between !important;
  align-items: center !important;
}
</style>
