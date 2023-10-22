<script setup lang="ts">
import { Html5QrcodeScanner } from 'html5-qrcode'

const state = ref({
  error: '',
})

function onScanSuccess(decodedText, decodedResult) {
  // Handle on success condition with the decoded text or result.
  console.log(`Scan result: ${decodedText}`, decodedResult)
}

onMounted(() => {
  const html5QrcodeScanner = new Html5QrcodeScanner(
    'reader',
    { fps: 10, qrbox: 300 },
    false,
  )

  html5QrcodeScanner.render(onScanSuccess, (err) => {
    if (err)
      state.value.error = err
  })

  document.querySelectorAll('#html5-qrcode-button-camera-permission').forEach((elm) => {
    console.log(elm)
    elm.className += ' button'
  })

  return async () => {
    await html5QrcodeScanner.clear()
  }
})
</script>

<template>
  <div class="flex flex-col items-center justify-center">
    <div id="reader" class="w-[90%]" style="border: none;" />
    <div v-if="state.error">
      {{ state.error }}
    </div>
  </div>
</template>

<style scoped>
:deep(#html5-qrcode-button-camera-stop) {
  display: none !important;
}

:deep(#reader div:first-child img:nth-child(2)) {
  display: none !important; /* Hide info button */
}

#reader {
  border: none !important;
}
</style>
