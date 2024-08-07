<script setup lang="ts">
import { useQueryClient } from '@tanstack/vue-query'
import { useIsCurrentUserLoaded } from 'vuefire'

useDark()

useSeoMeta({
  description: 'The SST Alumni App Admin Panel',
})

useHeadSafe({
  titleTemplate: (titleChunk) => {
    return titleChunk ? `${titleChunk} - SSTAA` : 'SSTAA'
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

const queryClient = useQueryClient()
const auth = useCurrentUser()
const authLoaded = useIsCurrentUserLoaded()

const state = reactive({
  showLoginScreen: false,
})

onMounted(() => {
  const loggedOut = authLoaded.value && !auth.value?.uid

  setTimeout(() => {
    state.showLoginScreen = loggedOut
  }, 1000)
})

watch([authLoaded, auth], (values, _, onCleanup) => {
  const loggedOut = values[0] && !values[1]?.uid

  const timeout = setTimeout(() => {
    state.showLoginScreen = loggedOut
  }, 1000)

  if (!loggedOut)
    queryClient.invalidateQueries()

  onCleanup(() => clearTimeout(timeout))
})
</script>

<template>
  <div>
    <Toast position="bottom-center" />

    <header class="border-b border-b-solid border-b-gray-200 dark:border-b-gray-800 px-4 py-3">
      <div class="flex justify-between items-center">
        <span class="font-semibold">
          SSTAA Admin
        </span>

        <div class="flex items-center space-x-2">
          <NuxtLink to="/admin/members">
            <Button link label="Members" size="small" />
          </NuxtLink>
          <NuxtLink to="/admin/check-ins">
            <Button link label="Check ins" size="small" />
          </NuxtLink>
          <NuxtLink to="/admin/events">
            <Button link label="Events" size="small" />
          </NuxtLink>
        </div>
      </div>
    </header>

    <AdminLoginScreen v-model:opened="state.showLoginScreen" />

    <NuxtPage />
  </div>
</template>

<style>
html {
  font-family: sans-serif;
  font-size: 15px;
}

body {
  margin: 0;
}
</style>
