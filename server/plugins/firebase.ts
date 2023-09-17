import { cert, initializeApp } from 'firebase-admin/app'

export default defineNitroPlugin(async () => {
  initializeApp({
    credential: cert(JSON.parse(useRuntimeConfig().firebase.config)),
    databaseURL: useRuntimeConfig().firebase.databaseUrl,
  })
})
