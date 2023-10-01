import { useStorage } from '@vueuse/core'

const queryKeyFactory = {
  admission: (key: string) => ['sstaars', 'admission', key],
}

export const useSstaarsStore = createGlobalState(() => {
  return useStorage('sstaars:data', { previousEvents: {} as Record<string, string> })
})

export function useAdmission(key: string) {
  const app = useNuxtApp()
  return app.$apiQuery.getAdmission.useQuery(
    queryKeyFactory.admission(key),
    () => ({
      params: {
        key,
      },
    }),
  )
}
