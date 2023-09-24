<script setup lang="ts">
import { f7Link, f7NavLeft, f7NavTitle, f7Navbar, f7Page } from 'framework7-vue'
import type { Router } from 'framework7/types'

const props = defineProps<{
  f7route: Router.Route
}>()

const { data: user } = useUser()

const showForbidden = computed(() => {
  if (!user.value) // Should load placeholder data
    return false
  return user.value.memberType !== 'exco'
})
</script>

<template>
  <f7Page>
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
      <AdminCommonLeftPanel :route="props.f7route" />
      <f7-block-title large class="ml-4">Events</f7-block-title>
      <AdminHomeEventsTable />
      <AdminHomeCreateEventPopup />
    </template>
  </f7Page>
</template>
