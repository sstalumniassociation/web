<script setup lang="ts">
import { f7Popup, f7Page, f7Navbar, f7NavRight, f7Link, f7BlockTitle, f7Block, f7List, f7Button, f7ListInput } from 'framework7-vue';
import { DeprecationTypes } from 'nuxt/dist/app/compat/capi';

const props = defineProps(["event"])

function epochToISO(epochTime: number) {
    const date = new Date(epochTime * 1000);
    
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');
    const hours = String(date.getHours()).padStart(2, '0');
    const minutes = String(date.getMinutes()).padStart(2, '0');

    return `${year}-${month}-${day}T${hours}:${minutes}`;
}

const updatedValues = reactive({
    name: props.event.name,
    description: props.event.description,
    location: props.event.location,
    badgeImage: props.event.badgeImage,
    startDateTime: epochToISO(props.event.startDateTime),
    endDateTime: epochToISO(props.event.endDateTime),
})

const state = reactive({
    pending: false,
    msg: ""
})

async function updateEvent() {
    console.log(updatedValues)
}

</script>

<template>
    <f7Popup class="update-event-popup">
        <f7Page>
            <f7Navbar title="Update Event">
                <f7NavRight>
                    <f7Link popup-close>Cancel</f7Link>
                </f7NavRight>
            </f7Navbar>
            <f7Page>
                <f7BlockTitle large>{{ `Update Event: ${ props.event.name }` }}</f7BlockTitle>
                <f7Block>
                    Some content goes here
                </f7Block>
                <f7List form @submit.prevent="updateEvent">
                    {{ console.log(epochToISO(props.event.endDateTime)) }}
                    <f7ListInput label="Event ID" v-model:value="props.event.id" disabled />
                    <f7ListInput label="Event Name" v-model:value="updatedValues.name" :placeholder="props.event.name" />
                    <f7ListInput label="Event Description" v-model:value="updatedValues.description" :placeholder="props.event.description" />
                    <f7ListInput label="Event Location" v-model:value="updatedValues.location" :placeholder="props.event.location" />
                    <f7ListInput label="Image URL" v-model:value="updatedValues.badgeImage" :placeholder="props.event.url" type="url" />
                    <f7ListInput label="Start Date and Time" v-model:value="updatedValues.startDateTime" :placeholder="epochToISO(props.event.startDateTime)" type="datetime-local" />
                    <f7ListInput label="End Date and Time" v-model:value="updatedValues.endDateTime" :placeholder="epochToISO(props.event.endDateTime)" type="datetime-local" />

                    <f7List inset>
                        <!-- <f7ListInput></f7ListInput> -->
                    </f7List>
                </f7List>
            </f7Page>
        </f7Page>
    </f7Popup>
</template>