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
        SSTAA
      </f7NavTitle>
      <f7NavTitleLarge>
        SSTAA
      </f7NavTitleLarge>
    </f7Navbar>

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
