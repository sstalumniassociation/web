<script setup lang="ts">
import { f7Button, f7Card, f7CardContent, f7CardHeader } from 'framework7-vue'
import type { ParseResult } from 'papaparse'
import { parse } from 'papaparse'
import { ref } from 'vue'
import { createId } from '@paralleldrive/cuid2'
import type { ImportedUser } from '~/shared/types'

const props = defineProps(['event'])

let uploadedAttendees: ImportedUser[]
const pending = ref(false)
const isUploaded = ref(false)
const error = ref({
  didError: false,
  msg: '',
})
const file = ref<File | undefined>(undefined)

// Check for Type
function isFile(file: File | undefined): file is File {
  return file instanceof File
}

function handleFileUpload(e: Event) {
  pending.value = true
  file.value = (e.target as HTMLInputElement).files?.[0]

  // Parse the CSV File
  if (isFile(file.value)) {
    parse(file.value, {
      skipEmptyLines: true,
      header: true,
      complete(results: ParseResult<ImportedUser>) {
        uploadedAttendees = results.data

        if (JSON.stringify(results.meta.fields?.sort()) !== JSON.stringify(['name', 'memberId', 'email', 'graduationYear', 'memberType'].sort())) {
          error.value.didError = true
          error.value.msg = 'CSV has missing fields! Please check the file and reupload it.'
          return
        }

        pending.value = uploadedAttendees === undefined
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
      graduationYear: Number.parseInt(item.graduationYear.toString(), 10),
    }))

    try {
      const res = await $api('/api/user/bulk', {
        method: 'POST',
        body: usersBatch,
      })

      createdUsers.push(res)
    }
    catch (err) {
      error.value.didError = true
      error.value.msg = `Failed to push chunk ${i} to the users database.`
    }
  }
  // Upload to users_event database
  for (let i = 0; i < createdUsers.length; i += 1) {
    const chunk = []
    for (let j = 0; j < createdUsers[i].length; j += 1) {
      chunk.push({
        userId: createdUsers[i][j].id,
        eventId: props.event.id,
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
      error.value.didError = true
      error.value.msg = `Failed to push chunk ${i} to the users_event database.`
    }
  }

  pending.value = false
  if (!error.value.didError)
    isUploaded.value = true
}
</script>

<template>
  <f7Card>
    <f7CardHeader>
      <div class="flex flex-col space-y-1">
        <span class="font-semibold text-2xl">Upload Attendees</span>
        <span class="text-base">Upload a <i>.csv</i> file containing the <b>Name, Email, Graduation Year and Member Type</b> of the attendees attending this event. Do ensure that this file contains headers for <b>all</b> the columns.</span>
      </div>
    </f7CardHeader>
    <f7CardContent>
      <div class="space-y-2">
        <div class="space-x-5">
          <label class="inline-block p-2 px-4 bg-[#007aff]/[0.4] hover:bg-[#007aff]/[0.7] duration-100 rounded-xl cursor-pointer">
            <input type="file" accept=".csv" class="display-none" @change="handleFileUpload($event)">
            <div class="space-x-2">
              <i class="icon f7-icons size-22">upload_circle_fill</i>
              <span class="text-md font-semibold">Upload File</span>
            </div>
          </label>
          <span><b>{{ file ? `Uploaded File:` : '' }}</b> {{ file ? file.name : '' }}</span>
        </div>
        <f7Button v-if="file" tonal class="flex-inline max-w-xs" :loading="pending" :disabled="pending" @click="uploadToDB">
          Populate Database
        </f7Button>
        <span v-if="isUploaded">Successfully added users to the database. </span>
        <span v-if="error.didError">{{ error.msg }}</span>
      </div>
    </f7CardContent>
  </f7Card>
</template>
