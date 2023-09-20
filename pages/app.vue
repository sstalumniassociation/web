<script setup lang="ts">
import 'framework7/css/bundle'
import 'framework7-icons/css/framework7-icons.css'
import 'material-icons/iconfont/material-icons.css'

// @ts-expect-error Missing types
import Framework7 from 'framework7/lite-bundle'

// @ts-expect-error Missing types
import Framework7Vue from 'framework7-vue/bundle'
import { f7App, f7View } from 'framework7-vue'

import { AppHomePage, LazyAppEventsPage } from '#components'

Framework7.use(Framework7Vue)

const appRoutes = [
  {
    path: '/',
    component: AppHomePage,
  },
  {
    path: '/events',
    component: LazyAppEventsPage,
  },
]

const route = useRoute()
const url = (() => {
  if (route.path.length === 4)
    return '/'
  return route.path.slice(4, route.path.length)
})()
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
      :url="url"
    />
  </f7App>
</template>
