<script setup lang="ts">
import { f7List, f7Page } from 'framework7-vue'
</script>

<template>
  <f7Page no-navbar>
    <f7List inset class="space-y-8">
      <div>
        <AppHomeMembershipCard />
      </div>

      <div>
        <AppHomeNews />
      </div>
    </f7List>
  </f7Page>
</template>
