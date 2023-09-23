<script setup lang="ts">
import { f7Block, f7BlockTitle, f7Icon, f7Link, f7List, f7ListItem, f7NavLeft, f7NavTitle, f7Navbar, f7Page, f7Panel, f7Tab, f7Tabs, f7Toolbar } from 'framework7-vue'
import type { Router } from 'framework7/types'
import { useIsCurrentUserLoaded } from 'vuefire'

defineProps<{
  f7router: Router.Router
}>()

const auth = useCurrentUser()
const authLoaded = useIsCurrentUserLoaded()
const { data: user } = useUser()

const state = reactive({
  showLoginScreen: false,
})

watch([authLoaded, auth], (values) => {
  setTimeout(() => { // This shows too fast, so it's better to lag for 1 second to prevent jarring layout changes
    state.showLoginScreen = values[0] && !values[1]
  }, 1000)
})

const showForbidden = computed(() => {
  return user.value?.memberType !== 'exco'
})
</script>

<template>
  <f7Page>
    <CommonLoginScreen v-model:opened="state.showLoginScreen" />
    <f7Navbar>
      <f7NavLeft v-if="!showForbidden">
        <f7Link panel-open="left" icon-ios="f7:menu" icon-md="material:menu" />
      </f7NavLeft>
      <f7NavTitle>
        SSTAA Admin
      </f7NavTitle>
    </f7Navbar>

    <LazyAdminHomeForbidden v-if="showForbidden" />

    <template v-else>
      <f7Panel left cover>
        <f7List menu-list strong-ios outline-ios>
          <f7ListItem selected link title="Events">
            <template #media>
              <f7Icon md="material:today" />
            </template>
          </f7ListItem>

          <f7ListItem link title="Members">
            <template #media>
              <f7Icon md="material:account_circle" />
            </template>
          </f7ListItem>

          <f7ListItem link title="Settings">
            <template #media>
              <f7Icon md="material:settings" />
            </template>
          </f7ListItem>
        </f7list>
      </f7Panel>

      <AdminHomeEventsTable />
    </template>
  </f7Page>
</template>
