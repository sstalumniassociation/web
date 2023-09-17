// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  devtools: { enabled: true },

  ssr: false, // Firebase *sighs*

  modules: [
    'nuxt-vuefire',
  ],

  vuefire: {
    config: {
      apiKey: 'AIzaSyC0JXbZ3JWmKC-cEaK3bUl8sQO1lShM1GA',
      authDomain: 'sstaa-app.firebaseapp.com',
      databaseURL: 'https://sstaa-app-default-rtdb.asia-southeast1.firebasedatabase.app',
      projectId: 'sstaa-app',
      storageBucket: 'sstaa-app.appspot.com',
      messagingSenderId: '717632543205',
      appId: '1:717632543205:web:e7918e4133d4cc209cf70c',
    },

    auth: {
      enabled: true,
      sessionCookie: true,
    },

    // appCheck: {
    //   debug: true,
    //   isTokenAutoRefreshEnabled: true,
    //   provider: 'ReCaptchaEnterprise',
    //   key: '6LfNWy8oAAAAAG9GdaqR-X8t8721YyHyILD_C6Pu',
    // },
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
