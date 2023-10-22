import { GrowthBook } from '@growthbook/growthbook'

export default defineNuxtPlugin(async () => {
  const config = useRuntimeConfig()

  const growthbook = new GrowthBook({
    apiHost: 'https://cdn.growthbook.io',
    clientKey: config.public.growthbook.clientKey,
    enableDevMode: true,
    subscribeToChanges: true,
  })

  await growthbook.loadFeatures()

  return {
    provide: {
      growthbook,
    },
  }
})
