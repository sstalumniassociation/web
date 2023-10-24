<script setup lang="ts">
import 'framework7/css/bundle'
import 'framework7-icons/css/framework7-icons.css'
import 'material-icons/iconfont/material-icons.css'

// @ts-expect-error Missing types
import Framework7 from 'framework7/lite-bundle'

// @ts-expect-error Missing types
import Framework7Vue from 'framework7-vue/bundle'

import { useIsCurrentUserLoaded } from 'vuefire'
import { AppHomePage, AppServicesEventPage } from '#components'

Framework7.use(Framework7Vue)

const appRoutes = [
  {
    name: 'home',
    path: '/app',
    component: AppHomePage,
  },
  {
    path: '/app/services/event/:id',
    component: AppServicesEventPage,
  },
]

const dark = useDark()

useSeoMeta({
  description: 'The SST Alumni App',
})

useHeadSafe({
  titleTemplate: (titleChunk) => {
    return titleChunk ? `${titleChunk} - SSTAA` : 'SSTAA'
  },
  meta: [
    {
      name: 'color-scheme',
      content: 'dark',
    },
  ],
  link: [
    {
      rel: 'icon',
      href: '/favicon.ico',
    },
    {
      rel: 'apple-touch-icon',
      href: '/apple-touch-icon-180x180.png',
      sizes: '180x180',
    },
    {
      rel: 'mask-icon',
      href: '/maskable-icon-512x512.png',
      color: '#ffffff',
    },
  ],
})

const route = useRoute()

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
  <VitePwaManifest />
  <F7App name="SSTAA" theme="md" :dark-mode="dark" :routes="appRoutes">
    <F7Views
      tabs
      class="safe-areas"
      :url="route.path"
      :master-detail-breakpoint="768"
      ios-swipe-back
      preload-previous-page
    >
      <CommonLoginScreen v-model:opened="state.showLoginScreen" />

      <F7Toolbar position="bottom" tabbar icons>
        <F7Link
          tab-link="#home"
          tab-link-active
          text="Home"
          icon-md="material:home"
        />
        <F7Link
          tab-link="#services"
          text="Services"
          icon-md="material:sparkle"
        />
        <F7Link
          tab-link="#events"
          text="Events"
          icon-md="material:events"
        />
        <F7Link
          tab-link="#profile"
          text="Profile"
          icon-md="material:account_circle"
        />
      </F7Toolbar>

      <F7View id="home" tab tab-active>
        <AppHomePage />
      </F7View>

      <F7View id="services" tab>
        <AppServicesPage />
      </F7View>

      <F7View id="events" tab>
        <AppEventsPage />
      </F7View>

      <F7View id="profile" tab>
        <AppProfilePage />
      </F7View>
    </F7Views>
  </F7App>
</template>
