<script setup lang="ts">
import { useIsCurrentUserLoaded } from 'vuefire'

useSeoMeta({
  title: 'Home',
})

const loaded = useIsCurrentUserLoaded()
const user = useCurrentUser()
</script>

<template>
  <AppHomeLoading v-if="!loaded" />
  <AppHomePage v-else-if="user" />
  <AppAuthPage v-else />
</template>
