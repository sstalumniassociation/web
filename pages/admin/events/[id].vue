<script setup lang="ts">
const route = useRoute()
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
            Upload users
          </h2>

          <UCard>
            <UButton>Upload</UButton>
          </UCard>
        </div>
      </section>
    </div>
  </div>
</template>
