import { useQuery, useQueryClient } from '@tanstack/vue-query'
import type { EventWithAttendees } from '~/shared/types'

const queryKeyFactory = {
  events: ['events'],
  event: (id: string) => ['events', id],
}

export function useEvents() {
  const firebaseCurrentUser = useCurrentUser()
  return useQuery({
    queryKey: queryKeyFactory.events,
    queryFn: () => $api<EventWithAttendees[]>('/api/event'),
    enabled: computed(() => !!firebaseCurrentUser.value), // Only run when user exists
  })
}

export function useEvent(id: string) {
  const firebaseCurrentUser = useCurrentUser()
  const queryClient = useQueryClient()
  return useQuery({
    queryKey: queryKeyFactory.events,
    queryFn: () => $api<EventWithAttendees>('/api/event'),
    enabled: computed(() => !!firebaseCurrentUser.value), // Only run when user exists
    placeholderData() {
      return queryClient.getQueryData<EventWithAttendees[]>(queryKeyFactory.events)?.find(event => event.id === id)
    },
  })
}