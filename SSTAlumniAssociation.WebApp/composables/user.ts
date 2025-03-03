import { useMutation, useQuery, useQueryClient } from '@tanstack/vue-query'

const queryKeyFactory = {
  currentUser: ['current-user'],
  users: ['users'],
}

export function useAdminApiGetUsers() {
  const firebaseCurrentUser = useCurrentUser()
  return useQuery({
    queryKey: queryKeyFactory.users,
    queryFn: () => $adminApiClient.user.get(),
    enabled: computed(() => !!firebaseCurrentUser.value), // Only run when user exists
  })
}

export function useUserSignOutMutation() {
  const auth = useFirebaseAuth()
  const queryClient = useQueryClient()
  return useMutation({
    mutationFn: async () => auth?.signOut(),
    onSuccess() {
      queryClient.clear()
    },
  })
}

export function useBulkCreateUserMutation() {
  return useMutation({
    mutationFn: $adminApiClient.userBatch.post,
  })
}
