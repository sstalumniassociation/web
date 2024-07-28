<script setup lang="ts">
import { useDatabase } from 'vuefire'
import { ref as dbRef } from 'firebase/database'
import type { User } from '~/shared/types'

const route = useRoute()
const router = useRouter()
const toast = useToast()
const dayjs = useDayjs()

const state = ref({
  showDeleteConfirmation: false,
})

const db = useDatabase()
const { data: checkedInUsersList } = useDatabaseList<{ $value: number }>(dbRef(db, route.params.id as string))
const { data: event, isPending: eventIsPending } = useEvent(route.params.id as string)
const { mutate: createUsersMutate, isPending: createUsersIsPending } = useBulkCreateUserMutation()
const { mutate: addEventUsersMutate, isPending: addEventUsersIsPending } = useAddEventUsersMutation(route.params.id as string)
const { mutate: deleteEventMutate, isPending: deleteEventIsPending } = useDeleteEventMutation(route.params.id as string)

const showPending = computed(() => createUsersIsPending.value || addEventUsersIsPending.value)

const links = computed(() => [
  {
    label: 'Events',
    route: '/admin/events',
  },
  {
    label: `${event.value?.name}`,
  },
])

const dropdownMenu = ref()

const dropdownItems = [
  {
    label: 'Download analytics',
    async command() {
      await downloadAnalytics()
    },
  },
  {
    label: 'Delete',
    command() {
      state.value.showDeleteConfirmation = true
    },
  },
]

type CsvRow = Pick<User, 'name' | 'email' | 'memberType' | 'graduationYear'>

const userUploadPreview = ref<CsvRow[]>([])
let users: CsvRow[] = []

async function parseCsvFile(event: Event) {
  users = []

  const target = event.target as HTMLInputElement
  if (!target.files || target.files.length < 1) {
    return toast.add({
      summary: 'No file selected',
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
        summary: 'Error parsing CSV file',
        detail: error.message,
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
              severity: 'success',
              summary: 'Users added',
              detail: 'Users have been added to the event',
            })
          },
          onError(error) {
            console.error(error)
            toast.add({
              summary: 'Error adding users to event',
              detail: error.message,
            })
          },
        },
      )
    },
    onError(error) {
      console.error(error)
      toast.add({
        summary: 'Error adding users to event',
        detail: error.message,
      })
    },
  })
}

// eslint-disable-next-line unused-imports/no-unused-vars
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

async function downloadAnalytics() {
  const checkedInAttendees = event.value?.attendees.map((attendee) => {
    const checkedIn = checkedInUsersList.value.find(a => a.id === attendee.admissionKey)

    return {
      email: attendee.email,
      checkedInAt: checkedIn ? dayjs.unix(checkedIn.$value).toISOString() : 'NA',
    }
  }) ?? []

  const { unparse } = await import('papaparse')
  const file = unparse(checkedInAttendees)

  const element = document.createElement('a')
  element.setAttribute('href', `data:text/plain;charset=utf-8,${encodeURIComponent(file)}`)
  element.setAttribute('download', `${event.value?.name ?? 'Untitled event'}.csv`)

  element.style.display = 'none'
  document.body.appendChild(element)
  element.click()
  document.body.removeChild(element)
}

function toggleDropdown(event: Event) {
  dropdownMenu.value.toggle(event)
}
</script>

<template>
  <div class="p-4">
    <div class="flex flex-col sm:flex-row sm:justify-between sm:items-center">
      <Breadcrumb :model="links" class="px-0">
        <template #item="{ item }">
          <NuxtLink v-if="item.route" :to="item.route">
            <Button link :label="(item.label as string)" class="px-0" />
          </NuxtLink>
          <span v-else>
            {{ item.label }}
          </span>
        </template>
      </Breadcrumb>

      <div class="flex gap-3 items-center">
        <Button label="Download roll call" @click="downloadRollCall" />

        <Button type="button" aria-haspopup="true" aria-controls="overlay_menu" icon="true" @click="toggleDropdown">
          <template #icon>
            <div class="i-lucide-more-horizontal mx-2" />
          </template>
        </Button>

        <Menu id="overlay_menu" ref="dropdownMenu" :model="dropdownItems" popup />
      </div>

      <Dialog v-model:visible="state.showDeleteConfirmation" modal header="Are you sure?" class="mx-4">
        <div class="flex flex-col space-y-6">
          <span>
            This action is irreversible. All event data will be deleted. All users will no longer be associated to this
            event.
          </span>

          <Message severity="warn" :closable="false">
            Enter the event name below to confirm deletion.
          </Message>

          <InputText :placeholder="event?.name" />
        </div>

        <template #footer>
          <div class="space-x-4">
            <Button :pending="deleteEventIsPending" severity="danger">
              Delete
            </Button>
            <Button variant="ghost" text @click="state.showDeleteConfirmation = false">
              Cancel
            </Button>
          </div>
        </template>
      </Dialog>
    </div>

    <div class="py-8 grid grid-cols-1 md:grid-cols-3 gap-4">
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
          <Skeleton v-for="idx in 5" :key="idx" class="w-full" height="2rem" />
        </template>

        <AdminEventUpdateForm v-else-if="event" v-bind="event" />
      </div>

      <section class="space-y-4 col-span-2">
        <h2 class="text-lg font-semibold">
          Attendees
        </h2>

        <input as="input" type="file" @change="parseCsvFile">

        <div v-if="userUploadPreview.length > 0" class="space-y-4">
          <span class="font-semibold">CSV preview</span>

          <DataTable :value="userUploadPreview">
            <Column field="name" header="Name" class="min-w-xs" />
            <Column field="email" header="Email" class="min-w-xs" />
            <Column field="graduationYear" header="Graduation Year" />
            <Column field="memberType" header="Member Type" />
          </DataTable>

          <Message severity="warn" :closable="false">
            If a user with the specified email already exists, a new user will not be created. The existing user will be
            added to the event.
          </Message>

          <Button :pending="showPending" @click="uploadUsers">
            Add users
          </Button>
        </div>

        <DataTable v-else-if="event?.attendees" :value="event.attendees">
          <Column field="admissionKey" header="Admission Key" class="min-w-xs">
            <template #body="{ data }">
              <code class="text-sm">
                {{ data.admissionKey }}
              </code>
            </template>
          </Column>
          <Column field="user.name" header="Name" class="min-w-xs" />
          <Column field="user.email" header="Email" class="min-w-xs" />
        </DataTable>
      </section>
    </div>
  </div>
</template>
