import { isDevelopment } from 'std-env'
import { $ } from 'execa'
import { logger } from '@nuxt/kit'

const ADMIN_API_INFO = {
  name: 'admin',
  url: process.env['services__admin-web-api__https__0'] || process.env['services__admin-web-api__http__0'] || process.env.NUXT_PUBLIC_API_URL,
}

const MEMBER_API_INFO = {
  name: 'member',
  url: process.env['services__member-web-api__https__0'] || process.env['services__member-web-api__http__0'] || process.env.NUXT_PUBLIC_API_URL,
}

const SERVICE_ACCOUNT_API_INFO = {
  name: 'service-account',
  url: process.env['services__service-account-web-api__https__0'] || process.env['services__service-account-web-api__http__0'] || process.env.NUXT_PUBLIC_API_URL,
}

// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  // Firebase
  ssr: false,

  devtools: { enabled: true },
  sourcemap: { client: true, server: true },
  spaLoadingTemplate: './app/spa-loading-template.html',

  hooks: {
    'nitro:build:before': async () => {
      const log = logger.create({
        defaults: {
          tag: 'build:before:kiota-codegen',
        },
      })

      const apis = [ADMIN_API_INFO, MEMBER_API_INFO, SERVICE_ACCOUNT_API_INFO]

      const tasks = apis.map(async ({ name, url }) => {
        log.log(`Generating Kiota code with endpoint ${url}`)
        const { stderr, stdout } = await $({ lines: true })`kiota generate -l typescript -d ${`${url}/swagger/v1/swagger.json`} -c ApiClient -o ./api/${name} --co`

        stderr.forEach(i => log.error(i))
        stdout.forEach(i => log.info(i))
      })

      await Promise.all(tasks)
    },
  },

  experimental: {
    // https://github.com/unjs/nitro/issues/1844
    appManifest: false,
  },

  typescript: {
    strict: true,
  },

  modules: [
    '@formkit/auto-animate/nuxt',
    'nuxt-vuefire',
    '@primevue/nuxt-module',
    '@unocss/nuxt',
    '@nuxtjs/critters',
    '@vite-pwa/nuxt',
    '@vueuse/nuxt',
    '~/modules/build-info',
  ],

  routeRules: {
    '/': { prerender: true },
  },

  primevue: {
    importTheme: { from: '@/themes/aura.js' },
  },

  unocss: {
    icons: {
      scale: 1.2,
      extraProperties: {
        'color': 'inherit',
        // Avoid crushing of icons in crowded situations
        'min-width': '1.2em',
      },
    },
  },

  app: {
    head: {
      script: [
        {
          innerHTML: `
            (function(c,l,a,r,i,t,y){
              c[a]=c[a]||function(){(c[a].q=c[a].q||[]).push(arguments)};
              t=l.createElement(r);t.async=1;t.src="https://www.clarity.ms/tag/"+i;
              y=l.getElementsByTagName(r)[0];y.parentNode.insertBefore(t,y);
            })(window, document, "clarity", "script", "jx23tb4eg4");`,
        },
      ],
      meta: [
        {
          name: 'viewport',
          content: 'width=device-width, initial-scale=1, maximum-scale=1, minimum-scale=1, user-scalable=no, viewport-fit=cover',
        },
        {
          name: 'apple-mobile-web-app-capable',
          content: 'yes',
        },
        {
          name: 'apple-mobile-web-app-status-bar-style',
          content: 'black-translucent',
        },
        {
          name: 'theme-color',
          content: '#000000',
        },
      ],
    },
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
      navigateFallbackDenylist: [
        /^\/$/, // No caching on root page
        /^\/admin/, // No caching on admin
        /^\/pass/, // No caching on passes
        /^\/cdn/, // No caching on CDN
      ],
    },
    devOptions: {
      enabled: process.env.VITE_DEV_PWA === 'true',
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

    public: {
      api: {
        member: '' || MEMBER_API_INFO.url,
        admin: '' || ADMIN_API_INFO.url,
        serviceAccount: '' || SERVICE_ACCOUNT_API_INFO.url,
      },

      growthbook: {
        clientKey: '' || process.env.NUXT_PUBLIC_GROWTHBOOK_CLIENT_KEY,
      },

      newRelic: {
        agentId: '' || process.env.NUXT_PUBLIC_NEW_RELIC_AGENT_ID,
        applicationId: '' || process.env.NUXT_PUBLIC_NEW_RELIC_APPLICATION_ID,
      },
    },
  },

  compatibilityDate: '2024-08-05',
})
