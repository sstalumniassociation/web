// https://github.com/nuxt/nuxt/issues/2561#issuecomment-358596224

export default defineNuxtRouteMiddleware((to, from) => {
  if (process.server)
    return

  if (to.fullPath.split('/')[1] !== from.fullPath.split('/')[1])
    window.location.href = to.fullPath
})
