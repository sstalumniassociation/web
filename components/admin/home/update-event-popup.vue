<script setup lang="ts">
import dayjs from 'dayjs'
import { f7Block, f7BlockTitle, f7Link, f7List, f7ListInput, f7NavRight, f7Navbar, f7Page, f7Popup } from 'framework7-vue'

const props = defineProps(['event'])

const updatedValues = reactive({
  name: props.event.name,
  description: props.event.description,
  location: props.event.location,
  badgeImage: props.event.badgeImage,
  startDateTime: dayjs(props.event.startDateTime * 1000).format('YYYY-MM-DDTHH:mm'),
  endDateTime: dayjs(props.event.endDateTime * 1000).format('YYYY-MM-DDTHH:mm'),
})

// const state = reactive({
//   pending: false,
//   msg: '',
// })

async function updateEvent() {
}
</script>

<template>
  <f7Popup class="update-event-popup">
    <f7Page>
      <f7Navbar title="Update Event">
        <f7NavRight>
          <f7Link popup-close>
            Cancel
          </f7Link>
        </f7NavRight>
      </f7Navbar>
      <f7Page>
        <f7BlockTitle large>
          {{ `Update Event: ${props.event.name}` }}
        </f7BlockTitle>
        <f7Block>
          Some content goes here
        </f7Block>
        <f7List form @submit.prevent="updateEvent">
          <f7ListInput :placeholer="props.event.id" label="Event ID" disabled />
          <f7ListInput v-model:value="updatedValues.name" label="Event Name" :placeholder="props.event.name" />
          <f7ListInput v-model:value="updatedValues.description" label="Event Description" :placeholder="props.event.description" />
          <f7ListInput v-model:value="updatedValues.location" label="Event Location" :placeholder="props.event.location" />
          <f7ListInput v-model:value="updatedValues.badgeImage" label="Image URL" :placeholder="props.event.url" type="url" />
          <f7List inset>
            <!-- <f7ListInput></f7ListInput> -->
          </f7List>
        </f7List>
      </f7Page>
    </f7Page>
  </f7Popup>
</template>
