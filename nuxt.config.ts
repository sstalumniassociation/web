import { isDevelopment } from 'std-env'

// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  ssr: false, // Firebase
  devtools: { enabled: true },

  modules: [
    'nuxt-vuefire',
    '@vite-pwa/nuxt',
  ],

  pwa: {
  },

  vuefire: {
    emulators: false,

    config: {
      projectId: 'sstaa-app' || process.env.FIREBASE_PROJECT_ID,
      apiKey: 'AIzaSyC0JXbZ3JWmKC-cEaK3bUl8sQO1lShM1GA' || process.env.FIREBASE_API_KEY,
      authDomain: 'sstaa-app.firebaseapp.com' || process.env.FIREBASE_AUTH_DOMAIN,
      databaseURL: 'https://sstaa-app-default-rtdb.asia-southeast1.firebasedatabase.app' || process.env.FIREBASE_DATABASE_URL,
      storageBucket: 'sstaa-app.appspot.com' || process.env.FIREBASE_STORAGE_BUCKET,
      messagingSenderId: '717632543205' || process.env.FIREBASE_MESSAGING_SENDER_ID,
      appId: '1:717632543205:web:e7918e4133d4cc209cf70c' || process.env.FIREBASE_APP_ID,
    },

    auth: {
      enabled: true,
    },

    appCheck: {
      debug: process.env.FIREBASE_APP_CHECK_DEBUG_TOKEN || isDevelopment,
      provider: 'ReCaptchaEnterprise' || process.env.FIREBASE_APP_CHECK_PROVIDER,
      key: '6LfNWy8oAAAAAG9GdaqR-X8t8721YyHyILD_C6Pu' || process.env.FIREBASE_APP_CHECK_KEY,
      isTokenAutoRefreshEnabled: true,
    },
  },

  runtimeConfig: {
    turso: {
      url: '' || process.env.TURSO_URL,
      authToken: '' || process.env.TURSO_AUTH_TOKEN,
    },

    firebase: {
      projectId: 'sstaa-app' || process.env.FIREBASE_PROJECT_ID,
    },
  },
})
