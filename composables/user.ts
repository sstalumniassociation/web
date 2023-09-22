import type { User } from '~/shared/types'

export function useUser() {
  const auth = useCurrentUser()
  return useApiFetch<User>(() => `/api/user/${auth.value?.uid}`, { immediate: false, watch: [() => auth.value?.uid] })
}
