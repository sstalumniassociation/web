<script setup lang="ts">
import 'framework7/css/bundle'
import 'framework7-icons/css/framework7-icons.css'
import 'material-icons/iconfont/material-icons.css'

// @ts-expect-error Missing types
import Framework7 from 'framework7/lite-bundle'

// @ts-expect-error Missing types
import Framework7Vue from 'framework7-vue/bundle'

Framework7.use(Framework7Vue)

const dark = useDark()
const route = useRoute()
</script>

<template>
  <F7App name="SSTAA Guard House Check Point" theme="md" :dark-mode="dark">
    <F7View
      main
      class="safe-areas"
      :url="route.path"
      :master-detail-breakpoint="768"
      ios-swipe-back
      preload-previous-page
    >
      <F7Page>
        <F7Navbar title="SSTAA Guard House Check Point" />
      </F7Page>
    </F7View>
  </F7App>
</template>
