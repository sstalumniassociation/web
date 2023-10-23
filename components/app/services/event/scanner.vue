<script setup lang="ts">
import { useQuery } from '@tanstack/vue-query'
import { f7Button, f7List, f7ListInput, f7ListItem } from 'framework7-vue'
import { Html5Qrcode } from 'html5-qrcode'

const emit = defineEmits(['scan'])

const state = ref({
  selectedCameraId: '',
})

const { data: cameras } = useQuery({
  queryKey: ['cameras'],
  queryFn: () => Html5Qrcode.getCameras(),
  refetchOnWindowFocus: false,
})

watchEffect((onCleanup) => {
  if (state.value.selectedCameraId.length === 0)
    return

  const html5Qrcode = new Html5Qrcode('reader')
  onCleanup(async () => {
    await html5Qrcode.stop()
    html5Qrcode.clear()
  })

  html5Qrcode.start(
    state.value.selectedCameraId,
    {
      fps: 10,
      qrbox: 250,
    },
    (decodedText) => {
      emit('scan', decodedText)
    },
    () => {}, // Ignore all errors, unlikely to happen
  )
})
</script>

<template>
  <div>
    <f7List inset>
      <f7ListItem v-if="cameras" title="Camera" smart-select :smart-select-params="{ openIn: 'popover' }">
        <select v-model="state.selectedCameraId" name="camera">
          <option v-for="{ id, label } in cameras" :key="id" :value="id">
            {{ label }}
          </option>
        </select>
      </f7ListItem>
    </f7List>

    <div id="reader" />
  </div>
</template>
