<script setup lang="ts">
import { f7Card, f7CardContent, f7CardFooter, f7List, f7SkeletonBlock } from 'framework7-vue'
import { useQRCode } from '@vueuse/integrations/useQRCode'

const dayjs = useDayjs()
const { width } = useWindowSize()

const { data: user, isLoading: userIsLoading } = useWhoAmI()
const { data: checkIns, refetch } = useMemberCheckIns()

const counter = useInterval(10000)
const { pause, resume } = useIntervalFn(refetch, 500, { immediate: false })

const latestCheckIn = computed(() => {
  return checkIns.value?.findLast(checkIn => checkIn.checkOutDateTime === undefined)
})

const latestCheckInDuration = computed(() => {
  if (!latestCheckIn.value)
    return null

  const _ = counter.value
  return dayjs().to(dayjs(latestCheckIn.value?.checkInDateTime), true)
})

const cardOpenedInner = ref(false)
const cardOpened = computed({
  get() {
    return !!latestCheckIn.value || cardOpenedInner.value
  },
  set(value) {
    if (latestCheckIn.value)
      return // If user has a check in, prevent them from closing the card to encourage check out
    cardOpenedInner.value = value
  },
})

watch(cardOpened, (value) => {
  if (value)
    resume()
  else
    pause()
}, { immediate: true })

watch(latestCheckIn, (value, oldValue) => {
  if (value && !oldValue)
    cardOpened.value = false
})

const membershipGradient: Record<string, string> = {
  // Associatea
  'c28780c6-d687-4bb8-b9ce-5fbca1e347c2': 'bg-gradient-to-br from-blue-500 to-blue-600',
  // Affiliate
  'd258488b-c5a3-4f96-add7-366be4934900': 'bg-gradient-to-br from-purple-500 to-purple-600',
  // Exco
  '7ad2dfda-82df-4597-a76f-40e5fd4fd28d': 'bg-gradient-to-br from-red-500 to-red-600',
  // Ordinary
  'c1869b12-56a9-4ed8-96d2-ef962c39799e': 'bg-gradient-to-br from-yellow-500 to-yellow-600',
}

const resolvedGradientClass = computed(() => {
  if (userIsLoading.value) {
    return null
  }

  if (user.value?.revoked) {
    return 'bg-gradient-to-br from-gray-500 to-gray-600'
  }

  if (!user.value?.discriminator) {
    throw new Error('Could not accurately determine user type.')
  }

  if ('activeSubscription' in user.value) {
    if (!user.value.activeSubscription?.membershipPlan?.name) {
      throw new Error('No subscription name')
    }
    return membershipGradient[user.value.activeSubscription?.membershipPlan?.id ?? '']
  }

  return {
    SystemAdmin: 'bg-gradient-to-br from-indigo-500 to-indigo-600',
    ServiceAccount: 'bg-gradient-to-br from-slate-500 from-slate-600',
  }[user.value?.discriminator]
})

const qrCode = useQRCode(() => latestCheckIn.value?.id ?? JSON.stringify({ user: user.value?.id ?? '' }), {
  width: 0.6 * width.value > 300 ? 300 : 0.6 * width.value,
})

function cardClicked() {
  cardOpened.value = !cardOpened.value
}

onMounted(() => {
  setTimeout(async () => {
    await $memberApiClient.auth.whoAmI.get()
  }, 3000)
})
</script>

<template>
  <div>
    <f7List v-if="userIsLoading" inset class="h-64">
      <f7SkeletonBlock class="rounded-md" effect="fade" height="100%" />
    </f7List>

    <f7Card v-else-if="user" @click="cardClicked">
      <f7CardContent
        :style="[cardOpened && { height: 'calc(70vh - calc(var(--f7-toolbar-height) + var(--f7-safe-area-bottom)))' }]"
        class="rounded-[16px] transition-all duration-350 ease-out"
        :class="[{ 'h-50': !cardOpened, 'min-h-[300px]': cardOpened }, resolvedGradientClass]" valign="top"
      >
        <div v-auto-animate class="flex flex-col w-full h-full text-white dark:text-inherit">
          <div class="flex flex-col">
            <span class="font-bold text-3xl">
              {{ user.name }}
            </span>
            <span v-if="'activeSubscription' in user" class="font-mono">
              {{ user.memberId }}
            </span>
          </div>

          <div v-auto-animate="{ easing: 'ease-out', duration: 100 }" class="flex-1 flex">
            <div v-if="cardOpened" class="flex-1 flex flex-col items-center justify-center bg-white rounded-2xl my-4">
              <img :src="qrCode" alt="QR Code">
              <br>
              <div v-if="'graduationYear' in user" class="text-gray-800">
                <span class="font-semibold">
                  Class of
                  {{ user.graduationYear }}
                </span>
                <br>
                <span>
                  {{ user?.activeSubscription?.membershipPlan?.name }}
                  member
                </span>
              </div>
            </div>
          </div>

          <div v-if="'graduationYear' in user && !cardOpened" class="flex flex-col">
            <span class="font-semibold">
              Class of {{ user.graduationYear }}
            </span>
            <span>
              {{ user.activeSubscription?.membershipPlan?.name }}
              member
            </span>
          </div>

          <div v-else-if="cardOpened && latestCheckIn" class="flex flex-col">
            <span class="font-semibold">
              You're checked into SST!
            </span>
            <span>
              Remember to check out by scanning this QR code at the Guard House!
            </span>
            <br>
            <span>
              You've been in SST for {{ latestCheckInDuration }}.
            </span>
          </div>
        </div>
      </f7CardContent>

      <f7CardFooter v-if="!latestCheckIn">
        <span>
          <strong>Coming back?</strong>
          <br>
          <span>Tap on this card and present it to the security at the front gate.</span>
        </span>
      </f7CardFooter>
    </f7Card>
  </div>
</template>
