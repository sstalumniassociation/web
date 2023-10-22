<script setup lang="ts">
import { FetchError } from 'ofetch'
import { f7, f7Block, f7BlockTitle, f7Button, f7Icon, f7List, f7ListButton, f7ListInput, f7ListItem, f7NavTitle, f7NavTitleLarge, f7Navbar, f7Page, f7PageContent, f7Sheet } from 'framework7-vue'

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
    const data = await $api(`/api/event/${state.eventId}`)
    sstaarsStore.value.previousEvents[data.id] = data.name
    state.sheetOpened = false
    await nextTick(() => {
      f7.views.current.router.navigate(`/app/services/event/${data.id}`, { openIn: 'sheet' })
    })
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
</script>

<template>
  <f7Page>
    <f7Navbar large transparent :sliding="false">
      <f7NavTitle sliding>
        Alumni Services
      </f7NavTitle>
      <f7NavTitleLarge>
        Alumni Services
      </f7NavTitleLarge>
    </f7Navbar>

    <f7List v-if="$growthbook.isOn('sstaars')" strong inset>
      <f7ListItem class="font-semibold">
        SSTAA Registration System (SSTAARS)
      </f7ListItem>
      <f7ListItem>
        Helping out with an SSTAA event? Access the Event Registration System here.
      </f7ListItem>
      <f7ListButton link="/halp" class="custom-card" @click="state.sheetOpened = true">
        <span>
          Open
        </span>
        <f7Icon material="chevron_right" />
      </f7ListButton>
    </f7List>

    <f7Sheet v-model:opened="state.sheetOpened" style="height: auto" swipe-to-close backdrop push>
      <f7PageContent>
        <f7BlockTitle large>
          SSTAARS
        </f7BlockTitle>
        <f7Block>
          <p>
            SSTAARS is intended for volunteers helping with SST Alumni Association Event Registration.
          </p>
          <p>
            You will need to have an access code to access SSTAARS.
          </p>
          <p>
            Please reach out to your SSTAA contact if they did not provide you with an access code.
          </p>
        </f7Block>

        <f7List>
          <f7ListInput v-model:value="state.eventId" placeholder="Access code" />
          <div class="m-4">
            <f7Button large fill preloader :loading="state.pending" @click="click">
              Continue
            </f7Button>
          </div>
        </f7List>

        <f7BlockTitle>
          My events
        </f7BlockTitle>
        <f7List inset strong>
          <span v-if="Object.keys(sstaarsStore.previousEvents).length === 0" class="opacity-80">
            No previous events.
          </span>
          <f7ListItem v-for="name, id in sstaarsStore.previousEvents" :key="id" :title="name" :link="`/app/services/event/${id}`" />
        </f7List>
      </f7PageContent>
    </f7Sheet>
  </f7Page>
</template>

<style scoped>
:deep(.custom-card > .list-button) {
  display: flex !important;
  justify-content: space-between !important;
  align-items: center !important;
}
</style>
