export function useUser() {
  const auth = useCurrentUser()
  return useApiFetch(() => `/api/user/${auth.value?.uid}`, { immediate: false, watch: [() => auth.value?.uid] })
}
