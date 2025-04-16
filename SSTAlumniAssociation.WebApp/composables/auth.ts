import { useQuery } from '@tanstack/vue-query'

const queryKeyFactory = {
  whoAmI: (userId: string) => ['auth', 'whoAmI', userId],
}

export function useWhoAmI() {
  const firebaseCurrentUser = useCurrentUser()

  return useQuery({
    queryKey: computed(() => queryKeyFactory.whoAmI(firebaseCurrentUser.value?.uid ?? '')),
    queryFn: () => $memberApiClient.auth.whoAmI.get(),
    enabled: computed(() => !!firebaseCurrentUser.value), // Only run when user exists
    retry: false,
  })
}
