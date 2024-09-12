import { useMutation, useQuery } from '@tanstack/vue-query'

const queryKeyFactory = {
  checkIns: ['checkIns'],
}

export function useCheckInsWithAppScope() {
  const firebaseCurrentUser = useCurrentUser()
  return useQuery({
    queryKey: queryKeyFactory.checkIns,
    queryFn: () => $memberApiClient.v1.checkIn.get({ queryParameters: { scope: 'app' } }),
    enabled: computed(() => !!firebaseCurrentUser.value), // Only run when user exists
  })
}

export function useCheckInsWithAdminScope() {
  const firebaseCurrentUser = useCurrentUser()
  return useQuery({
    queryKey: queryKeyFactory.checkIns,
    queryFn: () => $memberApiClient.v1.checkIn.get({ queryParameters: { scope: 'admin' } }),
    enabled: computed(() => !!firebaseCurrentUser.value), // Only run when user exists
  })
}

export function useCreateCheckInMutation() {
  return useMutation({
    mutationFn: $memberApiClient.v1.checkIn.post,
  })
}

export function useCreateCheckOutMutation() {
  return useMutation({
    mutationFn: (id: string) => $memberApiClient.v1.checkIn.byId(id).checkOut.post(),
  })
}
