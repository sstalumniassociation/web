<script setup lang="ts">
    import { f7List, f7ListItem, f7Card, f7CardContent, f7CardHeader, f7Button } from 'framework7-vue';
    import { parse, ParseResult } from 'papaparse'
    import { ref } from 'vue'
    import { readFileSync } from 'fs';

    const props = defineProps(["event"])

    const uploadedAttendees = ref({})
    const pending = ref(false)
    const file = ref<File | undefined>(undefined)

    function isFile(file: File | undefined): file is File {
        return file instanceof File
    }


    function handleFileUpload(e: Event) {
        pending.value = true
        const files = (e.target as HTMLInputElement).files
        file.value = files?.[0]
        
        if (isFile(file.value)) {
            parse(file.value, {
                skipEmptyLines: true,
                header: true,
                complete: function (results: ParseResult<Record<string, unknown>>) {
                    console.log(results)
                }
            })
        }
    }

    function uploadToDB() {

    }
</script>

<template>
    <f7Card>
        <f7CardHeader>
            <div class="flex flex-col space-y-1">
                <span class="font-semibold text-2xl">Upload Attendees</span>
                <span class="text-base">Upload a <i>.csv</i> file containing the <b>name</b> and <b>email</b> of the attendees attending this event. Do ensure that this file contains headers for <b>both</b> Name and Email.</span>
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
            </div>
        </f7CardContent>
    </f7Card>
</template>