<script setup lang="ts">
import { f7Card, f7CardHeader, f7Chip, f7CardContent } from 'framework7-vue';
import { EventWithAttendees } from '~/shared/types';

const props = defineProps(["event"])

</script>

<template>
    <f7Card>
        <f7CardContent class="p-0! lg-rounded!">
            <div class="relative">
                <img :src="props.event.badgeImage" class="object-cover w-full max-h-60 object-top lg-rounded!">
                <div class="p-10 absolute z-10 inset-0 flex space-y-3 flex flex-col bg-gray-800/[0.3] backdrop-blur-s lg-rounded!">
                    <span class="font-bold text-3xl md:text-4xl">{{ props.event.name }}</span>
                    <f7Chip outline :text="`0 / ${event.attendees.length} Attendees Checked In`" class="flex-inline" />
                    <span>{{  }}</span>
                </div>
            </div>
            <div class="p-6 pb-3 pt-2">
                <p class="text-md">{{ props.event.description }}</p>
            </div>
        </f7CardContent>
    </f7Card>
</template>