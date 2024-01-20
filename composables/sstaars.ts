import { useQuery } from '@tanstack/vue-query'
import { useStorage } from '@vueuse/core'

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
