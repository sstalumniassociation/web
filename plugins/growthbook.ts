import { GrowthBook } from '@growthbook/growthbook'
import { isDevelopment } from 'std-env'

export default defineNuxtPlugin(async () => {
  const config = useRuntimeConfig()

  const growthbook = new GrowthBook({
    apiHost: 'https://cdn.growthbook.io',
    clientKey: config.public.growthbook.clientKey,
    enableDevMode: isDevelopment,
    subscribeToChanges: true,
  })

  await growthbook.loadFeatures()

  return {
    provide: {
      growthbook,
    },
  }
})
