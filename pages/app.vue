<script setup lang="ts">
// @ts-expect-error Missing types
import Framework7 from 'framework7/lite-bundle'

// @ts-expect-error Missing types
import Framework7Vue from 'framework7-vue/bundle'
import { f7App, f7View } from 'framework7-vue'

Framework7.use(Framework7Vue)
</script>

<template>
  <f7App :routes="appRoutes">
    <f7View
      main
      browser-history
      browser-history-separator=""
      :browser-history-initial-match="true"
      :browser-history-store-history="false"
    >
      <NuxtPage />
    </f7View>
  </f7App>
</template>
