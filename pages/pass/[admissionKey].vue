<script setup lang="ts">
import { f7AccordionContent, f7App, f7Block, f7BlockTitle, f7Button, f7List, f7ListItem, f7Navbar, f7Page, f7SkeletonBlock, f7View } from 'framework7-vue'
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
  <f7App theme="md">
    <f7View>
      <f7Page>
        <f7Navbar title="SST Alumni Association Passes" />

        <div v-if="admissionIsLoading">
          <f7List inset class="flex flex-gap-4 flex-col">
            <f7SkeletonBlock height="100px" class="rounded-md!" />
            <f7SkeletonBlock height="200px" class="rounded-md!" />
            <f7SkeletonBlock height="300px" class="rounded-md!" />
          </f7List>
        </div>

        <div v-else-if="error">
          <f7Block class="flex flex-col items-start">
            <f7BlockTitle large>
              Not found :/
            </f7BlockTitle>
            <span class="text-lg">
              Your pass was not found.
              <br>
              That's all we know.
            </span>
            <f7Button href="/app" external fill class="mt-10!">
              Return to app
            </f7Button>
          </f7Block>
        </div>

        <div v-else-if="admission">
          <f7BlockTitle>
            Pass info
          </f7BlockTitle>
          <f7Block strong inset>
            <p class="font-semibold">
              Event name
            </p>
            <p>
              {{ admission?.body.event.name }}
            </p>
          </f7Block>

          <f7BlockTitle>
            QR code
          </f7BlockTitle>
          <f7Block>
            <div class="w-full flex flex-col items-center">
              <img :src="qrcode" alt="QR Code" class="max-w-[200px]">
              <span>{{ admission?.body.admissionKey }}</span>
            </div>
          </f7Block>

          <f7BlockTitle>FAQs</f7BlockTitle>
          <f7List strong outline-ios dividers-ios inset-md accordion-list>
            <f7ListItem accordion-item title="What do I do with this QR code?">
              <f7AccordionContent>
                <f7Block>
                  <p>
                    Simply present it to the SSTAA volunteers when entering SST.
                  </p>
                </f7Block>
              </f7AccordionContent>
            </f7ListItem>

            <f7ListItem accordion-item title="Can I reuse this QR code?">
              <f7AccordionContent>
                <f7Block>
                  <p>
                    No, each QR code is single use only. Please refrain from leaving the venue after admission.
                  </p>
                </f7Block>
              </f7AccordionContent>
            </f7ListItem>
          </f7List>
        </div>
      </f7Page>
    </f7View>
  </f7App>
</template>
