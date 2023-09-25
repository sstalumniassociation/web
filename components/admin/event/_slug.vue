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
        <f7Navbar back-link="Back">
            <f7NavTitle>
                SSTAA Admin {{ props.f7route.params.id }}
            </f7NavTitle>
        </f7Navbar>

        <LazyAdminHomeForbidden v-if="showForbidden" />

        <template v-else>
            
        </template>
    </f7Page>
</template>
