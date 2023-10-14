<script setup lang="ts">
import 'tailwindcss/src/css/preflight.css'
</script>

<template>
  <div>
    <header class="border-b border-b-gray-200 dark:border-b-gray-800 p-3">
      <div class="container mx-auto">
        <span class="font-semibold">SSTAA</span>
      </div>
    </header>

    <div class="p-3">
      <div class="container mx-auto flex flex-col gap-3">
        <UButton to="/app" :prefetch="false">
          Open SSTAA App
        </UButton>
        <UButton to="/admin" :prefetch="false">
          Open SSTAA Admin Portal
        </UButton>
        <UButton to="/guard" :prefetch="false">
          Open SSTAA Guard House Portal
        </UButton>
      </div>
      <UNotifications />
    </div>
  </div>
</template>
