export const appRoutes = [
  {
    path: '/',
    asyncComponent: () => import('~/pages/index.vue'),
  },
  {
    path: '/events',
    asyncComponent: () => import('~/pages/index.vue'),
  },
]
