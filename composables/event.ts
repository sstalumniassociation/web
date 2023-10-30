import { useMutation, useQuery, useQueryClient } from '@tanstack/vue-query'
import type { Event, EventWithAttendees } from '~/shared/types'

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

export function useEvent(id: MaybeRef<string>) {
  const firebaseCurrentUser = useCurrentUser()
  const queryClient = useQueryClient()
  return useQuery({
    queryKey: queryKeyFactory.event(toValue(id)),
    queryFn: () => $api<EventWithAttendees>(`/api/event/${toValue(id)}`),
    enabled: computed(() => !!firebaseCurrentUser.value), // Only run when user exists
    placeholderData() {
      return queryClient.getQueryData<EventWithAttendees[]>(queryKeyFactory.events)?.find(event => event.id === id)
    },
  })
}

export function useCreateEventMutation() {
  const queryClient = useQueryClient()
  return useMutation({
    mutationFn: (body: Omit<Event, 'id'>) => $api('/api/event', { method: 'POST', body }),
    onSuccess() {
      queryClient.invalidateQueries({
        queryKey: queryKeyFactory.events,
      })
    },
  })
}
