import { useMutation, useQuery, useQueryClient } from '@tanstack/vue-query'

const queryKeyFactory = {
  events: ['events'],
  event: (id: string) => ['events', id],
  attendees: (id: string) => ['events', id, 'attendees'],
}

export function useMemberEvents() {
  const firebaseCurrentUser = useCurrentUser()
  return useQuery({
    queryKey: queryKeyFactory.events,
    queryFn: () => $memberApiClient.event.get(),
    enabled: computed(() => !!firebaseCurrentUser.value), // Only run when user exists
  })
}

export function useAdminEvents() {
  const firebaseCurrentUser = useCurrentUser()
  return useQuery({
    queryKey: queryKeyFactory.events,
    queryFn: () => $adminApiClient.event.get(),
    enabled: computed(() => !!firebaseCurrentUser.value), // Only run when user exists
  })
}

export function useMemberEvent(id: MaybeRef<string>) {
  const firebaseCurrentUser = useCurrentUser()
  return useQuery({
    queryKey: queryKeyFactory.event(toValue(id)),
    queryFn: () => $memberApiClient.event.byId(toValue(id)).get(),
    enabled: computed(() => !!firebaseCurrentUser.value), // Only run when user exists
  })
}

export function useAdminEvent(id: MaybeRef<string>) {
  const firebaseCurrentUser = useCurrentUser()
  return useQuery({
    queryKey: queryKeyFactory.event(toValue(id)),
    queryFn: () => $adminApiClient.event.byId(toValue(id)).get(),
    enabled: computed(() => !!firebaseCurrentUser.value), // Only run when user exists
  })
}

export function useMemberEventAttendees(id: MaybeRef<string>) {
  const firebaseCurrentUser = useCurrentUser()
  return useQuery({
    queryKey: queryKeyFactory.attendees(toValue(id)),
    queryFn: () => $memberApiClient.event.byId(toValue(id)).attendee.get(),
    enabled: computed(() => !!firebaseCurrentUser.value), // Only run when user exists
  })
}

export function useAdminCreateEventMutation() {
  const queryClient = useQueryClient()
  return useMutation({
    mutationFn: $adminApiClient.event.post,
    onSuccess() {
      queryClient.invalidateQueries({
        queryKey: queryKeyFactory.events,
      })
    },
  })
}

export function useAdminUpdateEventMutation(id: MaybeRef<string>) {
  const queryClient = useQueryClient()
  return useMutation({
    mutationFn: $adminApiClient.event.byId(toValue(id)).post,
    onSuccess() {
      queryClient.invalidateQueries({
        queryKey: queryKeyFactory.events,
      })
    },
  })
}

export function useAdminAddEventAttendeesMutation(id: string) {
  const queryClient = useQueryClient()
  return useMutation({
    mutationFn: $adminApiClient.event.byId(toValue(id)).attendeeBatch.post,
    onSuccess() {
      queryClient.invalidateQueries({
        queryKey: queryKeyFactory.event(id),
      })
    },
  })
}

export function useAdminDeleteEventMutation(id: string) {
  const queryClient = useQueryClient()
  return useMutation({
    mutationFn: $adminApiClient.event.byId(toValue(id)).delete,
    onSuccess() {
      queryClient.refetchQueries({
        queryKey: queryKeyFactory.events,
      })
      queryClient.removeQueries({
        queryKey: queryKeyFactory.event(id),
      })
    },
  })
}
