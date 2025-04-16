import { useMutation, useQuery } from '@tanstack/vue-query'

const queryKeyFactory = {
  checkIns: ['checkIns'],
}

export function useMemberCheckIns() {
  const firebaseCurrentUser = useCurrentUser()
  return useQuery({
    queryKey: queryKeyFactory.checkIns,
    queryFn: () => $memberApiClient.checkIn.get(),
    enabled: computed(() => !!firebaseCurrentUser.value), // Only run when user exists
  })
}

export function useAdminCheckIns() {
  const firebaseCurrentUser = useCurrentUser()
  return useQuery({
    queryKey: queryKeyFactory.checkIns,
    queryFn: () => $adminApiClient.checkIn.get(),
    enabled: computed(() => !!firebaseCurrentUser.value), // Only run when user exists
  })
}

export function useServiceAccountCreateCheckInMutation() {
  return useMutation({
    mutationFn: $serviceAccountApiClient.checkIn.post,
  })
}

export function useServiceAccountCreateCheckOutMutation() {
  return useMutation({
    mutationFn: (id: string) => $serviceAccountApiClient.checkIn.byId(id).checkOut.post(),
  })
}
