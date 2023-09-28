import { useStorage } from '@vueuse/core'
import { useQuery } from '@tanstack/vue-query'

const queryKeyFactory = {
  admission: (key: string) => ['sstaars', 'admission', key],
}

export const useSstaarsStore = createGlobalState(() => {
  return useStorage('sstaars:data', { previousEvents: {} as Record<string, string> })
})

export function useAdmission(key: string) {
  return useQuery({
    queryKey: queryKeyFactory.admission(key),
    queryFn: () => $api(`/api/admission/${key}`),
    retry: false,
  })
}
