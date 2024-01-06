import { del, get, set } from 'idb-keyval'
import { QueryClient, VueQueryPlugin, type VueQueryPluginOptions } from '@tanstack/vue-query'
import { experimental_createPersister } from '@tanstack/query-persist-client-core'

export default defineNuxtPlugin((nuxtApp) => {
  const queryClient = new QueryClient({
    defaultOptions: {
      queries: {
        gcTime: 1000 * 60,
        persister: experimental_createPersister({
          storage: {
            getItem: (key: string) => get(key),
            setItem: (key: string, value: string) => set(key, value),
            removeItem: (key: string) => del(key),
          },
        }),
      },
    },
  })
  const vueQueryOptions: VueQueryPluginOptions = {
    queryClient,
  }

  nuxtApp.vueApp.use(VueQueryPlugin, vueQueryOptions)
})
