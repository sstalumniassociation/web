<script setup lang="ts">
import { f7, f7BlockTitle, f7Link, f7List, f7ListItem, f7NavLeft, f7NavRight, f7NavTitle, f7NavTitleLarge, f7Navbar, f7Page, f7Searchbar, f7SkeletonBlock, f7SkeletonText } from 'framework7-vue'
import type { Router } from 'framework7/types'
import { useIsCurrentUserLoaded } from 'vuefire'

defineProps<{
  f7router: Router.Router
}>()

const auth = useCurrentUser()
const authLoaded = useIsCurrentUserLoaded()

const state = reactive({
  showLoginScreen: false,
})

watch([authLoaded, auth], (values) => {
  setTimeout(() => { // This shows too fast, so it's better to lag for 1 second to prevent jarring layout changes
    state.showLoginScreen = values[0] && !values[1]
  }, 1000)
})
</script>

<template>
  <f7Page>
    <CommonLoginScreen v-model:opened="state.showLoginScreen" />
    <f7Navbar large transparent :sliding="false">
      <f7NavTitle sliding>
        SSTAA Admin
      </f7NavTitle>
      <f7NavTitleLarge>
        SSTAA Admin
      </f7NavTitleLarge>
    </f7Navbar>

    <f7List inset class="space-y-8">
      <div class="space-y-3">
        <f7SkeletonBlock v-for="n in 3" :key="n" class="rounded-md" effect="fade" height="10rem" />
      </div>
    </f7List>
  </f7Page>
</template>
