<script setup lang="ts">
import { f7Block, f7Link, f7List, f7ListItem, f7NavRight, f7Navbar, f7Page, f7PageContent, f7Popup, f7Sheet, f7SkeletonBlock } from 'framework7-vue'
import { Html5Qrcode } from 'html5-qrcode'

const { data: cameras, isLoading } = useCameras()
const { mutate: mutateCheckIn } = useCreateCheckInMutation()
const { mutate: mutateCheckOut } = useCreateCheckOutMutation()

const innerCameraId = ref('')
const cameraId = computed({
  get() {
    if (innerCameraId.value)
      return innerCameraId.value
    if (!cameras.value?.length)
      return null
    return cameras.value[0].id
  },
  set(value) {
    innerCameraId.value = value ?? ''
  },
})

let html5Qrcode: Html5Qrcode | null = null
const scannerData = ref<string | object | null>(null)
const confirmationModalOpened = computed({
  get() {
    return !!scannerData.value
  },
  set(value) {
    if (!value)
      scannerData.value = null
  },
})

watch(cameraId, async (value, _, onCleanup) => {
  if (!value)
    return

  await nextTick(() => {
    html5Qrcode = new Html5Qrcode('reader')
    onCleanup(async () => {
      await html5Qrcode?.stop()
      html5Qrcode?.clear()
    })

    html5Qrcode.start(
      value,
      {
        fps: 5,
        qrbox: 250,
      },
      (decodedText) => {
        html5Qrcode?.pause()
        if (decodedText[0] === '{') {
          scannerData.value = JSON.parse(decodedText)
          // mutateCheckIn({
          //   checkIn: JSON.parse(decodedText),
          // })
        }
        else {
          scannerData.value = decodedText
          // mutateCheckOut(decodedText)
        }
      },
      () => { }, // Ignore all errors, unlikely to happen
    )
  })
}, { immediate: true })

function resumeScanner() {
  html5Qrcode?.resume()
  scannerData.value = null
}
</script>

<template>
  <f7List>
    <f7ListItem v-if="isLoading">
      <f7SkeletonBlock height="20rem" class="rounded-lg" effect="fade" />
    </f7ListItem>

    <f7ListItem v-else-if="cameras" title="Camera" smart-select :smart-select-params="{ openIn: 'popover' }">
      <select v-model="cameraId" name="camera">
        <option v-for="{ id, label } in cameras" :key="id" :value="id">
          {{ label }}
        </option>
      </select>
    </f7ListItem>

    <f7Popup v-model:opened="confirmationModalOpened" push @popup:close="resumeScanner">
      <f7Page>
        <f7Navbar title="Confirm this entry?">
          <f7NavRight>
            <f7Link popup-close>
              Close
            </f7Link>
          </f7NavRight>
        </f7Navbar>

        <f7PageContent>
          <f7Block>Nice</f7Block>
        </f7PageContent>
      </f7Page>
    </f7Popup>

    <div id="reader" />
  </f7List>
</template>
