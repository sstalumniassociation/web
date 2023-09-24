<script setup lang="ts">
import { f7Card, f7CardContent, f7CardFooter, f7SkeletonBlock } from 'framework7-vue'
import type { User } from '~/shared/types'

const { data: user, isLoading: userIsLoading } = useUser()

const membershipGradient: Record<Exclude<User['memberType'], null>, string> = {
  associate: 'bg-gradient-to-br from-blue-500 to-blue-600',
  affiliate: 'bg-gradient-to-br from-purple-500 to-purple-600',
  exco: 'bg-gradient-to-br from-red-500 to-red-600',
  ordinary: 'bg-gradient-to-br from-yellow-500 to-yellow-600',
  revoked: 'bg-gradient-to-br from-gray-500 to-gray-600',
}
</script>

<template>
  <div>
    <div v-if="userIsLoading" class="h-55">
      <f7SkeletonBlock class="rounded-md" effect="fade" height="100%" />
    </div>

    <f7Card v-else-if="user" class="m-0!">
      <f7CardContent class="h-45 rounded-[16px]" valign="top" :class="membershipGradient[user.memberType!]">
        <div class="flex flex-col w-full h-full text-white dark:text-inherit">
          <div class="flex flex-col flex-1">
            <span class="font-bold text-3xl">
              {{ user.name }}
            </span>
            <span class="font-mono">
              {{ user.memberId }}
            </span>
          </div>
          <div class="flex flex-col">
            <span class="font-semibold">
              Class of {{ user.graduationYear }}
            </span>
            <span>
              {{ user.memberType?.[0].toLocaleUpperCase() }}{{ user.memberType?.slice(1, user.memberType?.length) }}
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
