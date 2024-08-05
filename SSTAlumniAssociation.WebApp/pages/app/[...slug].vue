<script setup lang="ts">
import 'framework7/css/bundle'
import 'framework7-icons/css/framework7-icons.css'
import 'material-icons/iconfont/material-icons.css'

import Framework7 from 'framework7/lite-bundle'

import Framework7Vue from 'framework7-vue/bundle'
import { f7App, f7Link, f7Toolbar, f7View, f7Views } from 'framework7-vue'

import { useIsCurrentUserLoaded } from 'vuefire'
import { useQueryClient } from '@tanstack/vue-query'

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

const queryClient = useQueryClient()
const auth = useCurrentUser()
const authLoaded = useIsCurrentUserLoaded()

const state = reactive({
  showLoginScreen: false,
})

watch([authLoaded, auth], (values, _, onCleanup) => {
  const loggedOut = values[0] && !values[1]

  const timeout = setTimeout(() => {
    state.showLoginScreen = loggedOut
  }, 1000)

  if (!loggedOut)
    queryClient.invalidateQueries()

  onCleanup(() => clearTimeout(timeout))
})
</script>

<template>
  <VitePwaManifest />
  <f7App name="SSTAA" theme="md" :dark-mode="dark" :routes="appRoutes">
    <f7Views
      tabs
      class="safe-areas"
      :url="route.path"
      :master-detail-breakpoint="768"
      ios-swipe-back
      preload-previous-page
    >
      <AppLoginScreen v-model:opened="state.showLoginScreen" />

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
          text="Events"
          icon-md="material:events"
        />
        <f7Link
          tab-link="#profile"
          text="Profile"
          icon-md="material:account_circle"
        />
      </f7Toolbar>

      <f7View id="home" tab tab-active>
        <AppHomePage />
      </f7View>

      <f7View id="services" tab>
        <AppServicesPage />
      </f7View>

      <f7View id="events" tab>
        <AppEventsPage />
      </f7View>

      <f7View id="profile" tab>
        <AppProfilePage />
      </f7View>
    </f7Views>
  </f7App>
</template>
