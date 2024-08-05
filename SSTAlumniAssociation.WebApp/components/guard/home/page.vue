<script setup lang="ts">
import { f7List, f7ListItem, f7Navbar, f7Page } from 'framework7-vue'
</script>

<template>
  <f7Page>
    <f7Navbar title="SSTAA Guard House" />

    <f7List>
      <f7ListItem :reload-detail="true" link="/guard/check-in">
        Check in
      </f7ListItem>
    </f7List>
  </f7Page>
</template>
