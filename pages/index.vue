<script setup lang="ts">
const user = useCurrentUser()
</script>

<template>
  <div>{{ user }}</div>
</template>
