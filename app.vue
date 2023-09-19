<script setup lang="ts">
import Framework7 from 'framework7/lite-bundle'
import Framework7Vue from 'framework7-vue/bundle'
import { f7App, f7View } from 'framework7-vue'

Framework7.use(Framework7Vue)
</script>

<template>
  <f7App>
    <NuxtLoadingIndicator />
    <f7View>
      <NuxtLayout>
        <NuxtPage />
      </NuxtLayout>
    </f7View>
  </f7App>
</template>
