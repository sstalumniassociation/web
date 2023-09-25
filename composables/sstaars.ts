import { useStorage } from '@vueuse/core'

export const useSstaarsStore = createGlobalState(() => {
  return useStorage('sstaars:data', { previousEvents: {} as Record<string, string> })
})
