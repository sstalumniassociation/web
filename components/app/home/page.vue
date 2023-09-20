<script setup lang="ts">
import { f7Button, f7Link, f7Navbar, f7Page } from 'framework7-vue'
</script>

<template>
  <f7Page name="Home">
    <f7Navbar title="Home" />
    <f7Link href="/events">
      to events
    </f7Link>
  </f7Page>
</template>
