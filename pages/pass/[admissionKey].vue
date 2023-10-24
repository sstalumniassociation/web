<script setup lang="ts">
import 'framework7/css/bundle'
import 'framework7-icons/css/framework7-icons.css'
import 'material-icons/iconfont/material-icons.css'

// @ts-expect-error Missing types
import Framework7 from 'framework7/lite-bundle'

// @ts-expect-error Missing types
import Framework7Vue from 'framework7-vue/bundle'

import { useQRCode } from '@vueuse/integrations/useQRCode'

Framework7.use(Framework7Vue)

const route = useRoute()
const { data: admission, isLoading: admissionIsLoading, error } = useAdmission(route.params.admissionKey as string)
const qrcode = useQRCode(() => admission.value?.body.admissionKey ?? '', {
  width: 500,
  color: {
    light: '#fefbff',
  },
})
</script>

<template>
  <F7App theme="md">
    <F7View>
      <F7Page>
        <F7Navbar title="SST Alumni Association Passes" />

        <div v-if="admissionIsLoading">
          <F7List inset class="flex flex-gap-4 flex-col">
            <F7SkeletonBlock height="100px" class="rounded-md!" />
            <F7SkeletonBlock height="200px" class="rounded-md!" />
            <F7SkeletonBlock height="300px" class="rounded-md!" />
          </F7List>
        </div>

        <div v-else-if="error">
          <F7Block class="flex flex-col items-start">
            <F7BlockTitle large>
              Not found :/
            </F7BlockTitle>
            <span class="text-lg">
              Your pass was not found.
              <br>
              That's all we know.
            </span>
            <F7Button href="/app" external fill class="mt-10!">
              Return to app
            </F7Button>
          </F7Block>
        </div>

        <div v-else-if="admission">
          <F7BlockTitle>
            Pass info
          </F7BlockTitle>
          <F7Block strong inset>
            <p class="font-semibold">
              Event name
            </p>
            <p>
              {{ admission?.body.event.name }}
            </p>
          </F7Block>

          <F7BlockTitle>
            QR code
          </F7BlockTitle>
          <F7Block>
            <div class="w-full flex flex-col items-center">
              <img :src="qrcode" alt="QR Code" class="max-w-[200px]">
              <span>{{ admission?.body.admissionKey }}</span>
            </div>
          </F7Block>

          <F7BlockTitle>FAQs</F7BlockTitle>
          <F7List strong outline-ios dividers-ios inset-md accordion-list>
            <F7ListItem accordion-item title="What do I do with this QR code?">
              <F7AccordionContent>
                <F7Block>
                  <p>
                    Simply present it to the SSTAA volunteers when entering SST.
                  </p>
                </F7Block>
              </F7AccordionContent>
            </F7ListItem>

            <F7ListItem accordion-item title="Can I reuse this QR code?">
              <F7AccordionContent>
                <F7Block>
                  <p>
                    No, each QR code is single use only. Please refrain from leaving the venue after admission.
                  </p>
                </F7Block>
              </F7AccordionContent>
            </F7ListItem>
          </F7List>
        </div>
      </F7Page>
    </F7View>
  </F7App>
</template>
