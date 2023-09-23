<script setup lang="ts">
import { f7Link, f7List, f7NavTitle, f7NavTitleLarge, f7Navbar, f7Page, f7Tab, f7Tabs, f7Toolbar } from 'framework7-vue'
import type { Router } from 'framework7/types'
import { useIsCurrentUserLoaded } from 'vuefire'

defineProps<{
  f7router: Router.Router
}>()

const auth = useCurrentUser()
const authLoaded = useIsCurrentUserLoaded()

const state = reactive({
  showLoginScreen: false,
})

watch([authLoaded, auth], (values) => {
  setTimeout(() => { // This shows too fast, so it's better to lag for 1 second to prevent jarring layout changes
    state.showLoginScreen = values[0] && !values[1]
  }, 1000)
})
</script>

<template>
  <f7Page>
    <CommonLoginScreen v-model:opened="state.showLoginScreen" />

    <f7Toolbar position="bottom" tabbar icons>
      <f7Link
        tab-link="#home"
        tab-link-active
        text="Home"
        icon-md="material:home"
      />
      <f7Link
        tab-link="#services"
        text="Services"
        icon-md="material:sparkle"
      />
      <f7Link
        tab-link="#events"
        text="events"
        icon-md="material:events"
      />
      <f7Link
        tab-link="#profile"
        text="Profile"
        icon-md="material:account_circle"
      />
    </f7Toolbar>

    <f7Tabs>
      <f7Tab id="home" tab-active>
        <f7List inset class="space-y-8">
          <div>
            <AppHomeMembershipCard />
          </div>

          <div>
            <AppHomeNews />
          </div>
        </f7List>
      </f7Tab>

      <f7Tab id="services">
        <AppServicesPage />
      </f7Tab>

      <f7Tab id="events">
        <AppEventsPage />
      </f7Tab>

      <f7Tab id="profile">
        <AppProfilePage />
      </f7Tab>
    </f7Tabs>
  </f7Page>
</template>
