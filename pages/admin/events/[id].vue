<script setup lang="ts">
import type { User } from '~/shared/types'

const route = useRoute()
const toast = useToast()
const { data: event, isPending: eventIsPending } = useEvent(route.params.id as string)

const links = computed(() => [
  {
    label: 'Events',
    to: '/admin/events',
  },
  {
    label: `${event.value?.name}`,
  },
])

type CsvRow = Pick<User, 'name' | 'email' | 'memberType' | 'graduationYear'>

const userUploadPreview = ref<CsvRow[]>([])

async function parseCsvFile(event: Event) {
  const target = event.target as HTMLInputElement
  if (!target.files || target.files.length < 1) {
    return toast.add({
      title: 'No file selected',
    })
  }

  const { parse } = await import('papaparse')
  const users: CsvRow[] = []

  parse<{ name: string, email: string, memberType: string, graduationYear: string }>(target.files[0], {
    header: true,
    step(results) {
      if (
        !results.data.email
        || !results.data.name
        || !results.data.memberType
        || !results.data.graduationYear
      ) {
        console.error('Invalid row', results)
        throw new Error('Invalid row')
      }

      users.push({
        name: results.data.name,
        email: results.data.email,
        memberType: results.data.memberType as User['memberType'],
        graduationYear: Number.parseInt(results.data.graduationYear),
      })
    },
    error(error) {
      console.error(error)
      toast.add({
        title: 'Error parsing CSV file',
        description: error.message,
      })
    },
    complete() {
      userUploadPreview.value = users.slice(0, 5)
    },
  })
}

function uploadUsers() {

}
</script>

<template>
  <div class="p-4">
    <UBreadcrumb :links="links" />

    <div class="py-8 grid grid-cols-1 md:grid-cols-2 gap-4">
      <div class="space-y-4">
        <div
          class="rounded-xl min-h-[200px] bg-center bg-cover bg-no-repeat relative" :style="{
            backgroundImage: event?.badgeImage ? `url(${event.badgeImage})` : undefined,
          }"
        >
          <div class="inset-0 absolute bg-black/40 flex items-center justify-center p-4">
            <h1 class="font-semibold text-4xl text-center">
              {{ event?.name }}
            </h1>
          </div>
        </div>

        <template v-if="eventIsPending">
          <USkeleton v-for="idx in 5" :key="idx" class="w-full h-8" />
        </template>

        <AdminEventUpdateForm v-else-if="event" v-bind="event" />
      </div>

      <section class="space-y-4">
        <div class="space-y-4">
          <h2 class="text-lg font-semibold">
            Attendees
          </h2>

          <input as="input" type="file" @change="parseCsvFile">

          <template v-if="userUploadPreview.length > 0">
            <UTable :rows="userUploadPreview" />
            <UButton @click="uploadUsers">
              Save
            </UButton>
          </template>
        </div>
      </section>
    </div>
  </div>
</template>
