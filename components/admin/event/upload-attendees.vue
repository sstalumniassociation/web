<script setup lang="ts">
    import { f7List, f7ListItem, f7Card, f7CardContent, f7CardHeader } from 'framework7-vue';
    const props = defineProps(["event"])
</script>

<template>
    <f7Card>
        <f7CardHeader>
            <div class="flex flex-col space-y-1">
                <span class="font-semibold text-2xl">Upload Attendees</span>
                <span class="text-base">Upload a <i>.csv</i> file containing the <b>name</b> and <b>email</b> of the attendees attending this event.</span>
            </div>
        </f7CardHeader>
        <f7CardContent>
            Upload CSV goes here
        </f7CardContent>
    </f7Card>
</template>