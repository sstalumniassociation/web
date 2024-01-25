<script setup lang="ts">
import type { User } from '~/shared/types'

const route = useRoute()
const router = useRouter()
const toast = useToast()

const state = ref({
  showDeleteConfirmation: false,
})

const { data: event, isPending: eventIsPending } = useEvent(route.params.id as string)
const { mutate: createUsersMutate, isPending: createUsersIsPending } = useBulkCreateUserMutation()
const { mutate: addEventUsersMutate, isPending: addEventUsersIsPending } = useAddEventUsersMutation(route.params.id as string)
const { mutate: deleteEventMutate, isPending: deleteEventIsPending } = useDeleteEventMutation(route.params.id as string)

const showPending = computed(() => createUsersIsPending.value || addEventUsersIsPending.value)

const links = computed(() => [
  {
    label: 'Events',
    to: '/admin/events',
  },
  {
    label: `${event.value?.name}`,
  },
])

const dropdownItems = [
  [
    {
      label: 'Delete',
      click() {
        state.value.showDeleteConfirmation = true
      },
    },
  ],
]

type CsvRow = Pick<User, 'name' | 'email' | 'memberType' | 'graduationYear'>

const userUploadPreview = ref<CsvRow[]>([])
let users: CsvRow[] = []

async function parseCsvFile(event: Event) {
  users = []

  const target = event.target as HTMLInputElement
  if (!target.files || target.files.length < 1) {
    return toast.add({
      title: 'No file selected',
    })
  }

  const { parse } = await import('papaparse')

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
  createUsersMutate(users, {
    onSuccess(r) {
      addEventUsersMutate(
        r.map(u => ({
          userId: u.id,
        })),
        {
          onSuccess() {
            toast.add({
              color: 'green',
              title: 'Users added',
              description: 'Users have been added to the event',
            })
          },
          onError(error) {
            console.error(error)
            toast.add({
              title: 'Error adding users to event',
              description: error.message,
            })
          },
        },
      )
    },
    onError(error) {
      console.error(error)
      toast.add({
        title: 'Error adding users to event',
        description: error.message,
      })
    },
  })
}

function deleteEvent() {
  deleteEventMutate(undefined, {
    onSuccess() {
      router.push('/admin/events')
    },
  })
}

async function downloadRollCall() {
  const { unparse } = await import('papaparse')
  const transformedData = event.value?.attendees.map(({ id, name, admissionKey, email }) => ({
    id,
    name,
    email,
    link: `https://app.sstaa.org/pass/${admissionKey}`,
  })) ?? []
  const file = unparse(transformedData)

  const element = document.createElement('a')
  element.setAttribute('href', `data:text/plain;charset=utf-8,${encodeURIComponent(file)}`)
  element.setAttribute('download', `${event.value?.name ?? 'Untitled event'}.csv`)

  element.style.display = 'none'
  document.body.appendChild(element)
  element.click()
  document.body.removeChild(element)
}
</script>

<template>
  <div class="p-4">
    <div class="flex justify-between items-center">
      <div>
        <UBreadcrumb :links="links" />
      </div>

      <div class="flex gap-3 items-center">
        <UButton @click="downloadRollCall">
          Download roll call
        </UButton>

        <UDropdown :items="dropdownItems">
          <UButton color="white" label="More" trailing-icon="i-heroicons-chevron-down-20-solid" />
        </UDropdown>
      </div>

      <UModal v-model="state.showDeleteConfirmation">
        <UCard>
          <template #header>
            <span class="font-semibold">
              Are you sure?
            </span>
          </template>

          This action is irreversible. All event data will be deleted. All users will no longer be associated to this event.

          <template #footer>
            <div class="space-x-4">
              <UButton :pending="deleteEventIsPending" @click="deleteEvent">
                Delete
              </UButton>
              <UButton variant="ghost" class="bg-transparent" @click="state.showDeleteConfirmation = false">
                Cancel
              </UButton>
            </div>
          </template>
        </UCard>
      </UModal>
    </div>

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
        <h2 class="text-lg font-semibold">
          Attendees
        </h2>

        <UFormGroup label="Add attendees">
          <input as="input" type="file" @change="parseCsvFile">
        </UFormGroup>

        <div v-if="userUploadPreview.length > 0" class="space-y-4">
          <span class="font-semibold">CSV preview</span>

          <UTable :rows="userUploadPreview" />

          <UAlert
            variant="subtle" color="yellow" title="Info"
            description="If a user with the specified email already exists, a new user will not be created. The existing user will be added to the event."
          />

          <UButton :pending="showPending" @click="uploadUsers">
            Add users
          </UButton>
        </div>

        <UTable v-else-if="event?.attendees" :rows="event.attendees" />
      </section>
    </div>
  </div>
</template>
