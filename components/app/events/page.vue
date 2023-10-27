<script setup lang="ts">
import { f7Block, f7NavTitle, f7NavTitleLarge, f7Navbar, f7Page, f7PageContent } from 'framework7-vue'
</script>

<template>
  <f7Page>
    <f7Navbar large transparent :sliding="false">
      <f7NavTitle sliding>
        Events & Bookings
      </f7NavTitle>
      <f7NavTitleLarge>
        Events & Bookings
      </f7NavTitleLarge>
    </f7Navbar>

    <f7Block class="text-xl opacity-75">
      Nothing here yet, check back soon for more!
    </f7Block>
  </f7Page>
</template>
