<script setup lang="ts">
import { VueQueryDevtools } from '@tanstack/vue-query-devtools'
</script>

<template>
  <div style="width: 100%; height: 100%">
    <NuxtLoadingIndicator />
    <NuxtLayout>
      <NuxtPage />
    </NuxtLayout>
    <VueQueryDevtools />
  </div>
</template>

<style>
#__nuxt {
  width: 100%;
  height: 100%;
}
</style>
