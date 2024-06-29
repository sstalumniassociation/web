<script setup lang="ts">
import { f7Page } from 'framework7-vue'
</script>

<template>
  <f7Page no-navbar>
    <div inset class="space-y-8">
      <div>
        <AppHomeMembershipCard />
      </div>

      <div>
        <LazyAppHomeNews />
      </div>
    </div>
  </f7Page>
</template>
