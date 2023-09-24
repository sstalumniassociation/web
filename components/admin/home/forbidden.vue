<script setup lang="ts">
import { f7Block, f7BlockTitle, f7Button, f7PageContent } from 'framework7-vue'
</script>

<template>
  <f7PageContent>
    <f7Block class="flex flex-col items-start">
      <f7BlockTitle large>
        Forbidden :/
      </f7BlockTitle>
      <span class="text-lg">
        You do not have access to this page.
        <br>
        That's all we know.
      </span>
      <f7Button href="/app" external fill class="mt-10!">
        Return to app
      </f7Button>
    </f7Block>
  </f7PageContent>
</template>
