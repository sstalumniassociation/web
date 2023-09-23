<script setup lang="ts">
import 'framework7/css/bundle'
import 'framework7-icons/css/framework7-icons.css'
import 'material-icons/iconfont/material-icons.css'

// @ts-expect-error Missing types
import Framework7 from 'framework7/lite-bundle'

// @ts-expect-error Missing types
import Framework7Vue from 'framework7-vue/bundle'
import { f7App, f7View } from 'framework7-vue'

import { AppHomePage } from '#components'

Framework7.use(Framework7Vue)

const appRoutes = [
  {
    name: 'home',
    path: '/app',
    component: AppHomePage,
  },
  {
    name: 'events',
    path: '/app/events',
    asyncComponent: () => import('~/components/app/events/page.vue'),
  },
]

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
    />
  </f7App>
</template>
