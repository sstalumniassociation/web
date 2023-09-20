import { isDevelopment } from 'std-env'

// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  ssr: false, // Firebase
  devtools: { enabled: true },

  modules: [
    'nuxt-vuefire',
    '@vite-pwa/nuxt',
  ],

  routeRules: {
    '/': { prerender: true },
  },

  pwa: {
    registerType: 'autoUpdate',
    client: {
      installPrompt: true,
      periodicSyncForUpdates: 60 * 60,
    },
    workbox: {
      navigateFallback: '/',
      globPatterns: ['**/*.{js,json,css,html,txt,svg,png,ico,webp,woff,woff2,ttf,eot,otf,wasm}'],
    },
    devOptions: {
      enabled: true,
      suppressWarnings: true,
      type: 'module',
    },
    manifest: {
      scope: '/app',
      start_url: '/app',
      name: 'SSTAA',
      short_name: 'SSTAA',
      description: 'The SST Alumni App',
      theme_color: '#000000',
      icons: [
        {
          src: '/pwa-64x64.png',
          sizes: '64x64',
          type: 'image/png',
        },
        {
          src: '/pwa-192x192.png',
          sizes: '192x192',
          type: 'image/png',
        },
        {
          src: '/pwa-512x512.png',
          sizes: '512x512',
          type: 'image/png',
        },

      ],
    },
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
