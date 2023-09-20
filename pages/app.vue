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

useHeadSafe({
  meta: [
    {
      name: 'viewport',
      content: 'width=device-width, initial-scale=1, maximum-scale=1, minimum-scale=1, user-scalable=no, viewport-fit=cover',
    },
    {
      name: 'apple-mobile-web-app-capable',
      content: 'yes',
    },
    {
      name: 'apple-mobile-web-app-status-bar-style',
      content: 'black-translucent',
    },
    {
      name: 'theme-color',
      content: '#000000',
    },
    {
      name: 'apple-touch-icon',
      content: 'logo.png',
    },
  ],
})

const route = useRoute()
</script>

<template>
  <f7App
    name="SSTAA"
    theme="md"
    dark-mode
    :routes="appRoutes"
  >
    <f7View
      main
      :url="route.path"
      :master-detail-breakpoint="768"
      preload-previous-page
      ios-swipe-back
    >
      <NuxtPage />
    </f7View>
  </f7App>
</template>
