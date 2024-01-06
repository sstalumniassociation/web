<script setup lang="ts">
const { data: users } = useUsers()
</script>

<template>
  <div class="p-4">
    <h1 class="font-semibold text-lg">
      Members
    </h1>

    <UTable :rows="users" />
  </div>
</template>
