import { useMutation, useQuery, useQueryClient } from '@tanstack/vue-query'
import type { User } from '~/shared/types'

const queryKeyFactory = {
  currentUser: ['current-user'],
  users: ['users'],
}

export function useUser() {
  const firebaseCurrentUser = useCurrentUser()
  return useQuery({
    queryKey: queryKeyFactory.currentUser,
    queryFn: () => $api<User>(`/api/user/${firebaseCurrentUser.value?.uid}`),
    enabled: computed(() => !!firebaseCurrentUser.value), // Only run when user exists
    retry: false,
  })
}

export function useUsers() {
  return useQuery({
    queryKey: queryKeyFactory.users,
    queryFn: () => $api(`/api/user`),
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
    mutationFn: (body: Partial<User>[]) => $api('/api/user/bulk', { method: 'post', body }),
  })
}
