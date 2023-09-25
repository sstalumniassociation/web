<script setup lang="ts">
import { f7Link, f7NavLeft, f7NavTitle, f7Navbar, f7Page } from 'framework7-vue'
import type { Router } from 'framework7/types'
import { EventWithAttendees } from '~/shared/types';

const props = defineProps<{
    f7route: Router.Route
}>()
console.log(props.f7route.params.id!)

const { data: user } = useUser()
const { data: event } = useEvent(props.f7route.params.id!)

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
                Event Details
            </f7NavTitle>
        </f7Navbar>

        <LazyAdminHomeForbidden v-if="showForbidden" />

        <template v-else>
            <div>
                <AdminEventInformation :event="event" />
            </div>

            <div>
                <AdminEventCheckedinUsers :event="event" />
            </div>
        </template>
    </f7Page>
</template>
