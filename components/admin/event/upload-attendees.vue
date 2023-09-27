<script setup lang="ts">
    import { f7List, f7ListItem, f7Card, f7CardContent, f7CardHeader, f7Button } from 'framework7-vue';
    import { parse, ParseResult } from 'papaparse'
    import { ref } from 'vue'
    import { createId } from '@paralleldrive/cuid2';
    import { ImportedUser } from '~/shared/types';

    const props = defineProps(["event"])

    let uploadedAttendees: ImportedUser[]
    const pending = ref(false)
    const isUploaded = ref(true)
    const didError = ref(false)
    const file = ref<File | undefined>(undefined)

    // Check for Type
    function isFile(file: File | undefined): file is File {
        return file instanceof File
    }
    
    async function handleFileUpload(e: Event) {
        pending.value = true
        const files = (e.target as HTMLInputElement).files
        file.value = files?.[0]
        
        // Parse the CSV File
        if (isFile(file.value)) {
            parse(file.value, {
                skipEmptyLines: true,
                header: true,
                complete: function (results: ParseResult<ImportedUser>) {
                    uploadedAttendees = results.data
                }
            })
        }

        await uploadToDB(uploadedAttendees)

        pending.value = false
        if (!didError) { isUploaded.value = true }
        
    }

    async function uploadToDB(data: unknown[]) {
        // Upload to users database
        const splittedUsers = [];
        for (let i = 0; i < uploadedAttendees.length; i += 30) {
            splittedUsers.push(uploadedAttendees.slice(i, i + 30));
        }

        let createdUsers = []
        for (let i = 0; i < splittedUsers.length; i++) {
            try {
                const res = await $api("/api/user/bulk", {
                    method: "POST",
                    body: splittedUsers[i]
                })

                createdUsers.push(res)
            } catch (err) {
                didError.value = true
                console.error(`Failed to push chunk ${i} to the users database.`)
            }
        }

        // Upload to users_event database
        for (let i = 0; i < splittedUsers.length; i += 30) {
            let chunk = []
            for (let j = 0; j < splittedUsers[i].length; i++) {
                chunk.push({
                    userId: createdUsers[i][j].id,
                    eventId: props.event.id,
                    admissionKey: createId()
                })
            }
            // Upload the chunk to the database
            try {
                await $api("/api/event/users", {
                    method: "POST",
                    body: chunk
                })
            } catch (err) {
                didError.value = true
                console.error(`Failed to push chunk ${i} to the users_event database.`)
            }
        }
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
                        <input type="file" accept=".csv" @change="handleFileUpload($event)" class="display-none"/>
                        <div class="space-x-2">
                            <i class="icon f7-icons size-22">upload_circle_fill</i>
                            <span class="text-md font-semibold">Upload File</span>
                        </div>
                    </label>
                    <span><b>{{ file ? `Uploaded File:` : '' }}</b> {{ file ? file.name : '' }}</span>
                </div>
                <f7Button v-if="file" tonal class="flex-inline max-w-xs" @click="uploadToDB" :loading="pending" :disabled="pending">
                    Populate Database
                </f7Button>
                <span v-if="isUploaded">Successfully added {{ uploadedAttendees.length }} users to the database. </span>
            </div>
        </f7CardContent>
    </f7Card>
</template>