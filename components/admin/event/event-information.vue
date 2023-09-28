<script setup lang="ts">
import { f7Card, f7CardContent } from 'framework7-vue'

const props = defineProps(['event'])

function convertEpochToSGT(epochTime: number) {
  const date = new Date(epochTime * 1000)
  date.setUTCHours(date.getUTCHours() + 8)

  const formattedDate = date.toLocaleDateString('en-SG', {
    day: '2-digit',
    month: '2-digit',
    year: 'numeric',
  })

  const formattedTime = date.toLocaleTimeString('en-SG', {
    hour: '2-digit',
    minute: '2-digit',
  })

  return `${formattedDate} | ${formattedTime}`
}
</script>

<template>
  <f7Card>
    <f7CardContent class="p-0! lg-rounded!">
      <div class="relative">
        <img :src="props.event.badgeImage" class="object-cover w-full max-h-60 object-top lg-rounded!">
        <div class="p-10 absolute z-10 inset-0 flex flex-col bg-gray-800/[0.3] space-y-6 backdrop-blur-sm lg-rounded!">
          <div class="space-y-3 flex flex-col">
            <span class="font-bold text-3xl md:text-4xl">{{ props.event.name }}</span>
            <div>
              <div class="flex-inline shrink grow-0 p-1 px-4 border border-solid b-rounded-3">
                {{ `0 / ${event.attendees.length}` }}
              </div>
            </div>
          </div>
          <div class="text-lg flex flex-col">
            <p><span class="font-bold">{{ convertEpochToSGT(props.event.startDateTime) }}</span> to <span class="font-bold">{{ convertEpochToSGT(props.event.endDateTime) }}</span></p>
            <p><span class="font-bold">Location:</span> {{ props.event.location }}</p>
          </div>
        </div>
      </div>
      <div class="p-6 pb-3 pt-2">
        <span class="text-xl font-bold">Description</span>
        <p class="text-md mt-2">
          {{ props.event.description }}
        </p>
      </div>
    </f7CardContent>
  </f7Card>
</template>
