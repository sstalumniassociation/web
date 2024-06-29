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

type ApiFetchRequest<T> = Ref<T> | T | (() => T)

export function useApiFetch<ResT, ReqT extends string = string>(request: ApiFetchRequest<ReqT>, options: UseFetchOptions<ResT> = {}) {
  const defaults: UseFetchOptions<ResT> = {
    $fetch: $api,
  }
  const params = defu(options, defaults)
  return useFetch(request, params)
}

export function useLazyApiFetch<ResT, ReqT extends string = string>(request: ApiFetchRequest<ReqT>, options: Omit<UseFetchOptions<ResT>, 'lazy'> = {}) {
  const defaults: UseFetchOptions<ResT> = {
    $fetch: $api,
  }
  const params = defu(options, defaults)
  return useLazyFetch(request, params)
}
