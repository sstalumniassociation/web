<script setup lang="ts">
import { f7, f7Block, f7BlockTitle, f7Icon, f7Link, f7List, f7ListItem, f7NavLeft, f7NavTitle, f7Navbar, f7Page, f7Panel, f7Tab, f7Tabs, f7Toolbar } from 'framework7-vue'
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
  <f7Panel left cover>
    <f7Page>
      <f7List menu-list strong-ios outline-ios>
        <f7ListItem v-for="link in menuLinks" :key="link.path" :link="link.path" :selected="selectedPath === link.path" :title="link.title" @click="selectedPath = link.path">
          <template #media>
            <f7Icon :md="link.icon" />
          </template>
        </f7ListItem>
      </f7List>
    </f7Page>
  </f7Panel>
</template>
