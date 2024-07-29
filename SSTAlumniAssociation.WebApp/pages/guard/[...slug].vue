<script setup lang="ts">
import { f7App, f7Navbar, f7Page, f7View } from 'framework7-vue'
import 'framework7/css/bundle'
import 'framework7-icons/css/framework7-icons.css'
import 'material-icons/iconfont/material-icons.css'

import Framework7 from 'framework7/lite-bundle'
import Framework7Vue from 'framework7-vue/bundle'

Framework7.use(Framework7Vue)

const dark = useDark()
const route = useRoute()
</script>

<template>
  <f7App name="SSTAA Guard House Check Point" theme="md" :dark-mode="dark">
    <f7View
      main
      class="safe-areas"
      :url="route.path"
      :master-detail-breakpoint="768"
      ios-swipe-back
      preload-previous-page
    >
      <f7Page>
        <f7Navbar title="SSTAA Guard House Check Point" />
      </f7Page>
    </f7View>
  </f7App>
</template>
