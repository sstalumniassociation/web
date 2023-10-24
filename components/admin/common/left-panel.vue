<script setup lang="ts">
import type { Router } from 'framework7/types'

const props = defineProps<{
  route: Router.Route
}>()

const selectedPath = ref(props.route.path)

const menuLinks = [
  {
    title: 'Events',
    icon: 'material:today',
    path: '/admin',
  },
  {
    title: 'Members',
    icon: 'material:account_circle',
    path: '/admin/members',
  },
  {
    title: 'Settings',
    icon: 'material:settings',
    path: '/admin/settings',
  },
]
</script>

<template>
  <F7Panel left cover>
    <F7Page>
      <F7List menu-list strong-ios outline-ios>
        <F7ListItem v-for="link in menuLinks" :key="link.path" :link="link.path" :selected="selectedPath === link.path" :title="link.title" @click="selectedPath = link.path">
          <template #media>
            <F7Icon :md="link.icon" />
          </template>
        </F7ListItem>
      </F7List>
    </F7Page>
  </F7Panel>
</template>
