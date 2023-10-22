import { initQueryClient } from '@ts-rest/vue-query'
import { VueQueryPlugin, type VueQueryPluginOptions } from '@tanstack/vue-query'
import { persistQueryClient } from '@tanstack/query-persist-client-core'
import { createSyncStoragePersister } from '@tanstack/query-sync-storage-persister'
import { contract } from '~/shared/contracts'

export default defineNuxtPlugin((nuxtApp) => {
  const vueQueryOptions: VueQueryPluginOptions = {
    queryClientConfig: {
      defaultOptions: {
        queries: {
          staleTime: 1000 * 60,
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

  const query = initQueryClient(contract, {
    baseUrl: '',
    baseHeaders: {},
  })

  return {
    provide: {
      apiQuery: query,
    },
  }
})
