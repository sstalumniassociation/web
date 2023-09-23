import { useMutation, useQuery, useQueryClient } from '@tanstack/vue-query'
import type { User } from '~/shared/types'

const queryKeyFactory = {
  user: ['user'],
}

export function useUser() {
  const firebaseCurrentUser = useCurrentUser()
  return useQuery({
    queryKey: queryKeyFactory.user,
    queryFn: () => $api<User>(`/api/user/${firebaseCurrentUser.value?.uid}`),
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
