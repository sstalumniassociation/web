import { VueQueryPlugin, type VueQueryPluginOptions } from '@tanstack/vue-query'
import { persistQueryClient } from '@tanstack/query-persist-client-core'
import { createSyncStoragePersister } from '@tanstack/query-sync-storage-persister'

export default defineNuxtPlugin((nuxtApp) => {
  const vueQueryOptions: VueQueryPluginOptions = {
    queryClientConfig: {
      defaultOptions: {
        queries: {
          cacheTime: 1000 * 60 * 60 * 24,
          staleTime: 1000 * 60 * 60 * 24,
        },
      },
    },
    clientPersister: (queryClient) => {
      return persistQueryClient({
        queryClient,
        persister: createSyncStoragePersister({ storage: localStorage }),
      })
    },
  }

  nuxtApp.vueApp.use(VueQueryPlugin, vueQueryOptions)
})
