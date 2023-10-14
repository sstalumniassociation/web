<script setup lang="ts">
import 'tailwindcss/src/css/preflight.css'
import { useIsCurrentUserLoaded } from 'vuefire'

const { data: user } = useUser()

const dark = useDark()

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
  <div class="p-6">
    <CommonLogin v-model:opened="state.showLoginScreen" />
    <AdminHomeEventsTable />
  </div>
</template>
