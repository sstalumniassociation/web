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
    queryFn: () => $fetch<Blob>(`/cdn/apple-wallet/${admission.value?.eventId}/${key}.pkpass`, { method: 'GET' }),
    // queryFn: () => $fetch<Blob>(`https://sstaa-cdn.qinguan.me/apple-wallet/${admission.value?.eventId}/${key}.pkpass`, { method: 'GET' }),
    // @ts-expect-error Apple Pay Session browser global https://developer.apple.com/documentation/apple_pay_on_the_web/apple_pay_js_api/checking_for_apple_pay_availability
    enabled: computed(() => !!admission.value?.admissionKey && !!window.ApplePaySession),
    retry: false,
  })
}
