<script setup lang="ts">
    import { f7List, f7ListItem, f7Card, f7CardContent, f7CardHeader } from 'framework7-vue';
    const props = defineProps(["event"])
</script>

<template>
    <f7Card>
        <f7CardHeader>
            <span class="font-semibold text-2xl">Attendees</span>
        </f7CardHeader>
        <f7CardContent>
            <f7List v-for="attendee in props.event.attendees" simple-list dividers-ios strong inset>
                <f7ListItem>
                    <div>
                        <span class="text-md">{{ attendee.name }}</span>
                        <span class="font-light">{{ attendee.id }}</span>
                    </div>
                </f7ListItem>
            </f7List>
        </f7CardContent>
    </f7Card>
</template>