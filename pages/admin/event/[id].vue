<script setup lang="ts">
import 'tailwindcss/src/css/preflight.css'

const route = useRoute()

const { data: event } = useEvent(route.params.id)
console.log(event)
</script>

<template>
  <div>
    <h1>{{ event.name }}</h1>
  </div>
</template>
