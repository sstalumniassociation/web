export function useUser() {
  const auth = useCurrentUser()
  return useApiFetch('/api/user/abc', { immediate: false, watch: [() => auth.value?.uid] })
}
