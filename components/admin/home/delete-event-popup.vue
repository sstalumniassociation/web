<script setup lang="ts">
import { f7Block, f7BlockTitle, f7Button, f7Link, f7List, f7ListInput, f7NavRight, f7Navbar, f7Page, f7Popup } from 'framework7-vue'
import { useMutation } from '@tanstack/vue-query'

const props = defineProps(['event'])

const state = reactive({
  verification: '',
  msg: '',
})

const mutation = useMutation({
  mutationFn: (id) => $api(`/api/event/${id}`, {
      method: 'DELETE',
  })
})

async function deleteEvent() {
  await mutation.mutateAsync(props.event.id)
}

function resetRefs() {
  state.verification = ''
  state.msg = ''
  mutation.reset
}
</script>

<template>
  <f7Popup class="delete-event-popup">
    <f7Page>
      <f7Navbar title="Delete Event">
        <f7NavRight>
          <f7Link popup-close>
            Cancel
          </f7Link>
        </f7NavRight>
      </f7Navbar>
      <f7Page>
        <f7BlockTitle large>
          {{ `Delete Event: ${props.event.name}` }}
        </f7BlockTitle>
        <f7Block>
          <p class="text-4">
            This event will be <b>permanently deleted</b>, including the record of the attendees who attended the event.
          </p>
        </f7Block>
        <f7List form @submit.prevent="deleteEvent">
          <f7ListInput v-model:value="state.verification" :label="`To verify, type the Event Name (${props.event.name}) in the field below:`" :placeholder="props.event.name" required validate :pattern="props.event.name" :disabled="mutation.isSuccess.value" />

          <f7List inset>
            <f7Button v-if="!mutation.isSuccess.value" fill type="submit" preloader :loading="mutation.isLoading.value" :disabled="mutation.isLoading.value">
              Continue
            </f7Button>
            <f7Button v-if="mutation.isSuccess.value" fill popup-close @click="resetRefs">
              Close
            </f7Button>
            <p v-if="mutation.isSuccess.value" class="text-center">
              Delete successful! You can close this popup now.
            </p>
          </f7List>
        </f7List>
      </f7Page>
    </f7Page>
  </f7Popup>
</template>
