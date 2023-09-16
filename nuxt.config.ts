// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  devtools: { enabled: true },
  runtimeConfig: {
    turso: {
      url: '' || process.env.TURSO_URL,
      authToken: '' || process.env.TURSO_AUTH_TOKEN,
    },
  },
})
