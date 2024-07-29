import { useQuery } from '@tanstack/vue-query'

const queryKeyFactory = {
  checkIns: ['checkIns'],
}

export function useCheckIns() {
  const firebaseCurrentUser = useCurrentUser()
  return useQuery({
    queryKey: queryKeyFactory.checkIns,
    queryFn: () => $apiClient.v1.checkIn.get(),
    enabled: computed(() => !!firebaseCurrentUser.value), // Only run when user exists
  })
}
