<script setup lang="ts">
import '@unocss/reset/tailwind-compat.css'
import { useIsCurrentUserLoaded } from 'vuefire'

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
  <div>
    <header class="border-b-[1px] border-solid border-b-white/10">
      <div class="py-2 px-4 flex justify-between items-center">
        <span class="font-semibold">
          SSTAA Admin
        </span>

        <div class="flex items-center space-x-2">
          <UButton variant="link" to="/admin/members">
            Members
          </UButton>
          <UButton variant="link" to="/admin/events">
            Events
          </UButton>
        </div>
      </div>
    </header>

    <AdminLoginScreen v-model="state.showLoginScreen" />

    <NuxtPage />
  </div>
</template>
