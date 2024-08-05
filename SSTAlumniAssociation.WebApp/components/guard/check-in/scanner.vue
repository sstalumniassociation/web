<script setup lang="ts">
import { f7List, f7ListItem, f7SkeletonBlock } from 'framework7-vue'
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

watch(cameraId, async (value, _, onCleanup) => {
  if (!value)
    return

  await nextTick(() => {
    const html5Qrcode = new Html5Qrcode('reader')
    onCleanup(async () => {
      await html5Qrcode.stop()
      html5Qrcode.clear()
    })

    html5Qrcode.start(
      value,
      {
        fps: 5,
        qrbox: 250,
      },
      (decodedText) => {
        if (decodedText[0] === '{') {
          mutateCheckIn({
            checkIn: JSON.parse(decodedText),
          })
        }
        else {
          mutateCheckOut(decodedText)
        }
      },
      () => { }, // Ignore all errors, unlikely to happen
    )
  })
}, { immediate: true })
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

    <div id="reader" />
  </f7List>
</template>
