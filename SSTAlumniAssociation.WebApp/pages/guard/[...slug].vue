<script setup lang="ts">
import { f7App, f7View } from 'framework7-vue'
import 'framework7/css/bundle'
import 'framework7-icons/css/framework7-icons.css'
import 'material-icons/iconfont/material-icons.css'

import Framework7 from 'framework7/lite-bundle'
import Framework7Vue from 'framework7-vue/bundle'

import { useIsCurrentUserLoaded } from 'vuefire'
import { useQueryClient } from '@tanstack/vue-query'

import { GuardCheckInPage, GuardHomePage } from '#components'

Framework7.use(Framework7Vue)

const route = useRoute()

const auth = useCurrentUser()
const queryClient = useQueryClient()
const authLoaded = useIsCurrentUserLoaded()
const { data: user, isLoading: userIsLoading } = useWhoAmI()

const state = reactive({
  showLoginScreen: false,
})

watch([authLoaded, auth, userIsLoading, user], (values, _, onCleanup) => {
  const loggedOut = values[0] && !values[1]
  const isGuardHouse = !values[2] && values[3]?.serviceAccount?.serviceAccountType === 'GuardHouse'

  const timeout = setTimeout(() => {
    state.showLoginScreen = loggedOut || !isGuardHouse
  }, 1000)

  if (!loggedOut)
    queryClient.invalidateQueries()

  onCleanup(() => clearTimeout(timeout))
}, { immediate: true })

const appRoutes = [
  {
    name: 'home',
    path: '/guard',
    master: true,
    component: GuardHomePage,
    detailRoutes: [
      {
        name: 'check-in',
        path: '/guard/check-in',
        component: GuardCheckInPage,
      },
    ],
  },
]
</script>

<template>
  <VitePwaManifest />
  <f7App :routes="appRoutes" theme="md">
    <f7View
      main
      class="safe-areas"
      :url="route.path"
      :master-detail-breakpoint="768"
      ios-swipe-back
      preload-previous-page
    >
      <GuardLoginScreen v-model:opened="state.showLoginScreen" />
    </f7View>
  </f7App>
</template>
