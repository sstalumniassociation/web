import { useMutation, useQuery, useQueryClient } from '@tanstack/vue-query'

const queryKeyFactory = {
  events: ['events'],
  event: (id: string) => ['events', id],
}

export function useEvents() {
  const firebaseCurrentUser = useCurrentUser()
  return useQuery({
    queryKey: queryKeyFactory.events,
    queryFn: () => $apiClient.v1.events.get(),
    enabled: computed(() => !!firebaseCurrentUser.value), // Only run when user exists
  })
}

export function useEvent(id: MaybeRef<string>) {
  const firebaseCurrentUser = useCurrentUser()
  return useQuery({
    queryKey: queryKeyFactory.event(toValue(id)),
    queryFn: () => $apiClient.v1.events.byId(toValue(id)).get(),
    enabled: computed(() => !!firebaseCurrentUser.value), // Only run when user exists
  })
}

export function useCreateEventMutation() {
  const queryClient = useQueryClient()
  return useMutation({
    mutationFn: $apiClient.v1.events.post,
    onSuccess() {
      queryClient.invalidateQueries({
        queryKey: queryKeyFactory.events,
      })
    },
  })
}

export function useUpdateEventMutation(id: MaybeRef<string>) {
  const queryClient = useQueryClient()
  return useMutation({
    mutationFn: $apiClient.v1.events.byId(toValue(id)).patch,
    onSuccess() {
      queryClient.invalidateQueries({
        queryKey: queryKeyFactory.events,
      })
    },
  })
}

export function useAddEventUsersMutation(id: string) {
  const queryClient = useQueryClient()
  return useMutation({
    mutationFn: $apiClient.v1.events.byId(toValue(id)).attendees.post,
    onSuccess() {
      queryClient.invalidateQueries({
        queryKey: queryKeyFactory.event(id),
      })
    },
  })
}

export function useDeleteEventMutation(id: string) {
  const queryClient = useQueryClient()
  return useMutation({
    mutationFn: $apiClient.v1.events.byId(toValue(id)).delete,
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
