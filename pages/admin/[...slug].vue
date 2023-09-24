<script setup lang="ts">
import 'framework7/css/bundle'
import 'framework7-icons/css/framework7-icons.css'
import 'material-icons/iconfont/material-icons.css'

// @ts-expect-error Missing types
import Framework7 from 'framework7/lite-bundle'

// @ts-expect-error Missing types
import Framework7Vue from 'framework7-vue/bundle'
import { f7App, f7View } from 'framework7-vue'

import { useIsCurrentUserLoaded } from 'vuefire'
import { AdminHomePage, AdminMembersPage } from '#components'

Framework7.use(Framework7Vue)

const appRoutes = [
  {
    name: 'home',
    path: '/admin',
    component: AdminHomePage,
  },
  {
    name: 'members',
    path: '/admin/members',
    component: AdminMembersPage,
  },
]

useSeoMeta({
  description: 'SST Alumni App Admin',
})

useHeadSafe({
  titleTemplate: (titleChunk) => {
    return titleChunk ? `${titleChunk} - SSTAA Admin` : 'SSTAA Admin'
  },
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
  <f7App name="SSTAA" theme="md" dark-mode :routes="appRoutes">
    <f7View
      main
      class="safe-areas"
      :url="route.path"
      :master-detail-breakpoint="768"
      ios-swipe-back
      preload-previous-page
    >
      <CommonLoginScreen v-model:opened="state.showLoginScreen" />
    </f7View>
  </f7App>
</template>
