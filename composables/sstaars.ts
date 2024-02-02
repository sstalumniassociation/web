import { useQuery } from '@tanstack/vue-query'
import { useStorage } from '@vueuse/core'

const queryKeyFactory = {
  admission: (key: string) => ['sstaars', 'admission', key],
  admissionPkPass: (key: string) => ['sstaars', 'admission', key, 'pkPass'],
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

export function useAdmissionPkPass(key: string) {
  const { data: admission } = useAdmission(key)
  return useQuery({
    queryKey: queryKeyFactory.admissionPkPass(key),
    // queryFn: () => $fetch(`/cdn/apple-wallet/${admission.value?.eventId}/${key}.pkpass`, { method: 'GET' }),
    queryFn: () => $fetch<Blob>(`https://sstaa-cdn.qinguan.me/apple-wallet/${admission.value?.eventId}/${key}.pkpass`, { method: 'GET' }),
    enabled: computed(() => !!admission.value?.admissionKey),
    retry: false,
  })
}
