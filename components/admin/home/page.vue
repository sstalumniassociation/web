<script setup lang="ts">
import { f7Block, f7BlockTitle, f7Link, f7Navbar, f7Page, f7Tab, f7Tabs, f7Toolbar } from 'framework7-vue'
import type { Router } from 'framework7/types'
import { useIsCurrentUserLoaded } from 'vuefire'

defineProps<{
  f7router: Router.Router
}>()

const auth = useCurrentUser()
const authLoaded = useIsCurrentUserLoaded()
const { data: user } = useUser()

const state = reactive({
  showLoginScreen: false,
})

watch([authLoaded, auth], (values) => {
  setTimeout(() => { // This shows too fast, so it's better to lag for 1 second to prevent jarring layout changes
    state.showLoginScreen = values[0] && !values[1]
  }, 1000)
})

const showForbidden = computed(() => {
  return user.value?.memberType !== 'exco'
})
</script>

<template>
  <f7Page>
    <CommonLoginScreen v-model:opened="state.showLoginScreen" />
    <f7Navbar title="SSTAA Admin" />

    <LazyAdminHomeForbidden v-if="showForbidden" />

    <f7Toolbar v-else position="bottom" tabbar icons>
      <f7Link
        tab-link="#events"
        tab-link-active
        text="Events"
        icon-md="material:today"
      />
      <f7Link
        tab-link="#members"
        text="Members"
        icon-md="material:account_circle"
      />
      <f7-link
        tab-link="#settings"
        text="Settings"
        icon-md="material:settings"
      />
    </f7Toolbar>

    <f7Tabs>
      <f7Tab id="events" tab-active>
        <f7BlockTitle large>
          Events
        </f7BlockTitle>
        <AdminHomeEventsTable />
      </f7Tab>

      <f7Tab id="members">
        <f7BlockTitle large>
          Members
        </f7BlockTitle>
      </f7Tab>

      <f7Tab id="settings">
        <f7BlockTitle large>
          Settings
        </f7BlockTitle>
      </f7Tab>
    </f7Tabs>
  </f7Page>
</template>
