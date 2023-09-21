import { defu } from 'defu'
import { getAuth } from 'firebase/auth'
import type { UseFetchOptions } from '#app'

export const $api = $fetch.create({
  async onRequest(context) {
    const token = await getAuth().currentUser?.getIdToken()
    if (token) {
      context.options.headers = {
        Authorization: `Bearer ${token}`,
      }
    }
  },
})

export function useApiFetch<T>(url: string, options: UseFetchOptions<T> = {}) {
  const defaults: UseFetchOptions<T> = {
    $fetch: $api,
  }
  const params = defu(options, defaults)
  return useFetch(url, params)
}

export function useLazyApiFetch<T>(url: string, options: UseFetchOptions<T> = {}) {
  const defaults: UseFetchOptions<T> = {
    $fetch: $api,
  }
  const params = defu(options, defaults)
  return useLazyFetch(url, params)
}
