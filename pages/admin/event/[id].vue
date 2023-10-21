<script setup lang="ts">
import 'tailwindcss/src/css/preflight.css'
import dayjs from 'dayjs'
import type { ParseResult } from 'papaparse'
import { parse } from 'papaparse'
import { ref } from 'vue'
import { createId } from '@paralleldrive/cuid2'
import { useQueryClient } from '@tanstack/vue-query'
import type { User } from '~/shared/types'

const route = useRoute()
const queryClient = useQueryClient()

const { data: event } = useEvent(route.params.id as string)

// Display the date in a more readable way, e.g. '20 Oct 2023 | 13:23'
const displayedStartDate = computed(() => {
  if (event.value)
    return `${dayjs(Number.parseInt(event.value.startDateTime) * 1000).format('D MMM YYYY')} | ${dayjs(Number.parseInt(event.value.startDateTime) * 1000).format('HH:mm')}`
  else
    return 'Invalid Date'
})

const displayedEndDate = computed(() => {
  if (event.value)
    return `${dayjs(Number.parseInt(event.value.endDateTime) * 1000).format('D MMM YYYY')} | ${dayjs(Number.parseInt(event.value.endDateTime) * 1000).format('HH:mm')}`
  else
    return 'Invalid Date'
})

// Upload Attendees
let uploadedAttendees: Omit<User, 'firebaseId' | 'id'>[]

interface stateTypes {
  pending: boolean
  isUploaded: boolean
  msg: string
  file?: File
}

const state = ref<stateTypes>({
  pending: false,
  isUploaded: false,
  msg: '',
  file: undefined,
})

// Check for Type
function isFile(file: File | undefined): file is File {
  return file instanceof File
}

function handleFileUpload(e: Event) {
  state.value.pending = true
  state.value.file = (e.target as HTMLInputElement).files?.[0]

  // Parse the CSV File
  if (isFile(state.value.file)) {
    parse(state.value.file, {
      skipEmptyLines: true,
      header: true,
      complete(results: ParseResult<Omit<User, 'firebaseId' | 'id'>>) {
        uploadedAttendees = results.data

        if (JSON.stringify(results.meta.fields?.sort()) !== JSON.stringify(['name', 'email', 'memberType', 'graduationYear'].sort())) {
          state.value.msg = 'CSV has missing fields! Please check the file and reupload it.'
          return
        }

        state.value.pending = uploadedAttendees === undefined
      },
    })
  }
}

async function uploadToDB() {
  // Upload to users database
  const createdUsers = []

  for (let i = 0; i < uploadedAttendees.length; i += 30) {
    let usersBatch = uploadedAttendees.slice(i, i + 29)

    usersBatch = usersBatch.map(item => ({
      ...item,
      graduationYear: Number.parseInt((item.graduationYear).toString(), 10),
      memberId: createId(),
    }))

    try {
      const res = await $api('/api/user/bulk', {
        method: 'POST',
        body: usersBatch,
      })

      createdUsers.push(res)
    }
    catch (err) {
      state.value.msg = `Failed to push chunk ${i} to the users database.`
    }
  }
  // Upload to users_event database
  for (let i = 0; i < createdUsers.length; i += 1) {
    const chunk = []
    for (let j = 0; j < createdUsers[i].length; j += 1) {
      chunk.push({
        userId: createdUsers[i][j].id,
        eventId: event.value ? event.value.id : '',
        admissionKey: createId(),
      })
    }
    // Upload the chunk to the database
    try {
      await $api('/api/event/users', {
        method: 'POST',
        body: chunk,
      })
    }
    catch (err) {
      state.value.msg = `Failed to push chunk ${i} to the users_event database.`
    }
  }

  state.value.pending = false
  if (state.value.msg.length === 0) {
    state.value.isUploaded = true
    queryClient.invalidateQueries({ queryKey: ['events'] })
  }
}
</script>

<template>
  <div class="w-full py-3 border-b-2 flex flex-row border-gray-800">
    <div class="pl-6 absolute">
      <UButton color="gray" icon="i-heroicons-arrow-small-left" variant="ghost" @click="$router.go(-1)" />
    </div>
    <p class="text-2xl font-semibold ml-auto mr-auto">
      Event Detail
    </p>
  </div>
  <div v-if="!event" class="flex flex-col items-center justify-center pt-20 space-y-3">
    <h1 class="font-semibold text-3xl">
      Event cannot be found
    </h1>
    <p>We could not find an event with the ID of {{ route.params.id as string }}! Did you enter the wrong ID?</p>
    <NuxtLink to="/admin">
      <UButton color="blue" class="px-5 py-2">
        Back to Admin Home
      </UButton>
    </NuxtLink>
  </div>
  <UContainer v-if="event" class="py-3 space-y-7">
    <!-- Event Information -->
    <UCard>
      <template #header>
        <div class="relative">
          <img :src="event.badgeImage" class="object-cover w-full max-h-60 object-top rounded-xl">
          <div class="p-10 absolute z-10 inset-0 flex flex-col bg-gray-800/[0.3] space-y-6 backdrop-blur-sm rounded-xl">
            <div class="space-y-3 flex flex-col">
              <span class="font-bold text-3xl md:text-4xl">{{ event.name }}</span>
              <div>
                <div class="flex-inline shrink grow-0 p-1 px-4 border rounded-lg">
                  {{ `0 / ${event.attendees.length}` }}
                </div>
              </div>
            </div>
            <div class="text-lg flex flex-col">
              <span><span class="font-bold">{{ displayedStartDate }}</span> to <span class="font-bold">{{ displayedEndDate }}</span></span>
              <span><span class="font-bold">Location:</span> {{ event.location }}</span>
            </div>
          </div>
        </div>
      </template>
      <div class="">
        <span class="text-xl font-bold">Description</span>
        <p class="text-md mt-2">
          {{ event.description }}
        </p>
      </div>
    </UCard>

    <!-- Upload Attendees -->
    <UCard>
      <template #header>
        <div class="flex flex-col space-y-1">
          <span class="font-semibold text-2xl">Upload Attendees</span>
          <span class="text-base">Upload a <i>.csv</i> file containing the <span class="font-bold">Name, Email, Member Type and Graduation Year</span> of the attendees attending this event. Do ensure that this file contains headers for <span class="font-bold">all</span> the columns.</span>
        </div>
      </template>

      <div class="space-y-2">
        <div class="space-x-5">
          <label class="inline-block p-2 px-4 bg-[#007aff]/[0.4] hover:bg-[#007aff]/[0.7] duration-100 rounded-xl cursor-pointer">
            <input type="file" accept=".csv" class="hidden" @change="handleFileUpload($event)">
            <div class="space-x-2">
              <UIcon name="i-heroicons-arrow-up-tray" />
              <span class="text-md font-semibold">Upload File</span>
            </div>
          </label>
          <span><span class="font-bold">{{ state.file ? `Uploaded File:` : '' }}</span> {{ state.file ? state.file.name : '' }}</span>
        </div>
        <UButton v-if="state.file" tonal class="flex-inline max-w-xs" :loading="state.pending" :disabled="state.pending" @click="uploadToDB">
          Populate Database
        </UButton>
        <span v-if="state.isUploaded">Successfully added users to the database. </span>
        <span v-if="state.msg.length > 0">{{ state.msg }}</span>
      </div>
    </UCard>

    <!-- Checked in Attendees -->
    <UCard>
      <template #header>
        <span class="font-semibold text-2xl">Attendees</span>
      </template>
      <div v-if="event.attendees.length === 0" class="flex justify-center align-items-center">
        <div class="text-center">
          <UIcon class="text-5xl" name="i-heroicons-face-frown" />
          <p class="font-bold text-xl">
            No attendees yet!
          </p>
          <p>Upload attendees using a .csv file first!</p>
        </div>
      </div>
      <div v-if="event.attendees.length > 0">
        <div v-for="attendee in event.attendees" :key="attendee.id">
          <div class="mb-3">
            <p class="font-bold text-xl">
              {{ attendee.name }}
            </p>
            <p><span class="font-semibold">ID: </span>{{ attendee.id }}</p>
            <hr class="my-2">
          </div>
        </div>
      </div>
    </UCard>
  </UContainer>
</template>
