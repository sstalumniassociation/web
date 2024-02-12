<script setup lang="ts">
import { useIsCurrentUserLoaded } from 'vuefire'

const user = useCurrentUser()
const authLoaded = useIsCurrentUserLoaded()
</script>

<template>
  <div class="mx-auto container p-4">
    User debug:
    <br>
    <pre>{{ JSON.stringify({ user, authLoaded }, null, 2) }}</pre>
  </div>
</template>
