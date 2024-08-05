<script setup lang="ts">
import { f7Navbar, f7Page } from 'framework7-vue'
</script>

<template>
  <f7Page>
    <f7Navbar title="Check in" back-link />
    <GuardCheckInScanner />
  </f7Page>
</template>
