<script setup lang="ts">
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
  <F7Page>
    <F7Navbar>
      <F7NavLeft v-if="!showForbidden">
        <F7Link panel-open="left" icon-ios="f7:menu" icon-md="material:menu" />
      </F7NavLeft>
      <F7NavTitle>
        SSTAA Admin
      </F7NavTitle>
    </F7Navbar>

    <LazyAdminHomeForbidden v-if="showForbidden" />

    <template v-else>
      <AdminCommonLeftPanel :route="props.f7route" />
      <AdminHomeEventsTable />
    </template>
  </F7Page>
</template>
