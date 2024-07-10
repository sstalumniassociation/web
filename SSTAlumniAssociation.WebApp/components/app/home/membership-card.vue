<script setup lang="ts">
import { f7Card, f7CardContent, f7CardFooter, f7List, f7SkeletonBlock } from 'framework7-vue'
import type { Membership } from '~/api/models'

const { data: user, isLoading: userIsLoading } = useUser()

const membershipGradient: Record<Membership, string> = {
  Associate: 'bg-gradient-to-br from-blue-500 to-blue-600',
  Affiliate: 'bg-gradient-to-br from-purple-500 to-purple-600',
  Exco: 'bg-gradient-to-br from-red-500 to-red-600',
  Ordinary: 'bg-gradient-to-br from-yellow-500 to-yellow-600',
  Revoked: 'bg-gradient-to-br from-gray-500 to-gray-600',
}

const resolvedGradientClass = computed(() => {
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
</script>

<template>
  <div>
    <f7List v-if="userIsLoading" inset class="h-64">
      <f7SkeletonBlock class="rounded-md" effect="fade" height="100%" />
    </f7List>

    <f7Card v-else-if="user">
      <f7CardContent class="h-60 rounded-[16px]" valign="top" :class="resolvedGradientClass">
        <div class="flex flex-col w-full h-full text-white dark:text-inherit">
          <div class="flex flex-col flex-1">
            <span class="font-bold text-3xl">
              {{ user.name }}
            </span>
            <span v-if="user.member" class="font-mono">
              {{ user.member.memberId }}
            </span>
          </div>
          <div v-if="user.member" class="flex flex-col">
            <span class="font-semibold">
              Class of {{ user.member?.alumniMember?.graduationYear ?? user.member?.employeeMember?.graduationYear }}
            </span>
            <span>
              {{ user.member.membership }}
              member
            </span>
          </div>
        </div>
      </f7CardContent>
      <f7CardFooter>
        <span>
          <strong>Coming back?</strong>
          <br>
          <span>Tap on this card and present it to the security at the front gate.</span>
        </span>
      </f7CardFooter>
    </f7Card>
  </div>
</template>
