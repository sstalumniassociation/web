// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  devtools: { enabled: true },
  runtimeConfig: {
    turso: {
      url: '' || process.env.TURSO_URL,
      authToken: '' || process.env.TURSO_AUTH_TOKEN,
    },
    firebase: {
      config: '' || process.env.FIREBASE_CONFIG,
      databaseUrl: 'https://sstaa-app-default-rtdb.asia-southeast1.firebasedatabase.app' || process.env.FIREBASE_DATABASE_URL,
    },
  },
})
