<script setup lang="ts">
import { f7List, f7Page, f7SkeletonBlock } from 'framework7-vue'
</script>

<template>
  <f7Page>
    <f7List inset class="flex flex-col space-y-5">
      <f7SkeletonBlock height="2rem" effect="fade" />
      <f7SkeletonBlock height="5rem" effect="fade" />
      <f7SkeletonBlock height="10em" effect="fade" />
      <f7SkeletonBlock height="15rem" effect="fade" />
    </f7List>
  </f7Page>
</template>
