import { useMutation, useQuery, useQueryClient } from '@tanstack/vue-query'

const queryKeyFactory = {
  currentUser: ['current-user'],
  users: ['users'],
}

export function useUser() {
  const firebaseCurrentUser = useCurrentUser()
  return useQuery({
    queryKey: queryKeyFactory.currentUser,
    queryFn: () => $apiClient.v1.auth.whoami.get(),
    enabled: computed(() => !!firebaseCurrentUser.value), // Only run when user exists
    retry: false,
  })
}

export function useUsers() {
  const firebaseCurrentUser = useCurrentUser()
  return useQuery({
    queryKey: queryKeyFactory.users,
    queryFn: () => $apiClient.v1.users.get(),
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
    mutationFn: $apiClient.v1.usersBatchCreate.post,
  })
}
