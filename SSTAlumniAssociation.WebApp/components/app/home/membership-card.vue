<script setup lang="ts">
import { f7Card, f7CardContent, f7CardFooter, f7List, f7SkeletonBlock } from 'framework7-vue'
import { useQRCode } from '@vueuse/integrations/useQRCode'
import type { Membership } from '~/api/models'

const dayjs = useDayjs()
const { width } = useWindowSize()

const { data: user, isLoading: userIsLoading } = useWhoAmI()
const { data: checkIns, refetch } = useCheckInsWithAppScope()

const counter = useInterval(10000)
const { pause, resume } = useIntervalFn(refetch, 500, { immediate: false })

const latestCheckIn = computed(() => {
  return checkIns.value?.checkIns?.findLast(checkIn => checkIn.checkOutDateTime === undefined)
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

const membershipGradient: Record<Membership, string> = {
  Associate: 'bg-gradient-to-br from-blue-500 to-blue-600',
  Affiliate: 'bg-gradient-to-br from-purple-500 to-purple-600',
  Exco: 'bg-gradient-to-br from-red-500 to-red-600',
  Ordinary: 'bg-gradient-to-br from-yellow-500 to-yellow-600',
  Revoked: 'bg-gradient-to-br from-gray-500 to-gray-600',
}

const resolvedGradientClass = computed(() => {
  if (userIsLoading.value) {
    return null
  }
  if (user.value?.systemAdmin) {
    return 'bg-gradient-to-br from-indigo-500 to-indigo-600'
  }
  if (user.value?.serviceAccount) {
    return 'bg-gradient-to-br from-slate-500 from-slate-600'
  }
  if (user.value?.member && user.value?.member?.membership) {
    return membershipGradient[user.value?.member?.membership]
  }
  throw new Error('Could not accurately determine user type.')
})

const qrCode = useQRCode(() => latestCheckIn.value?.id ?? JSON.stringify({ user: user.value?.id ?? '' }), {
  width: 0.6 * width.value > 300 ? 300 : 0.6 * width.value,
  color: {
    light: '#0000',
    dark: '#fff',
  },
})

function cardClicked() {
  cardOpened.value = !cardOpened.value
}
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
        :class="[{ 'h-50': !cardOpened, 'min-h-[400px]': cardOpened }, resolvedGradientClass]" valign="top"
      >
        <div class="flex flex-col w-full h-full text-white dark:text-inherit">
          <div class="flex flex-col">
            <span class="font-bold text-3xl">
              {{ user.name }}
            </span>
            <span v-if="user.member" class="font-mono">
              {{ user.member.memberId }}
            </span>
          </div>
          <div class="flex-1 flex items-center justify-center">
            <img v-if="cardOpened" :src="qrCode" alt="QR Code">
          </div>

          <div v-if="user.member && !cardOpened" class="flex flex-col">
            <span class="font-semibold">
              Class of {{ user.member?.alumniMember?.graduationYear ?? user.member?.employeeMember?.graduationYear }}
            </span>
            <span>
              {{ user.member.membership }}
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
