// @ts-expect-error Bad types
import { verifyIdToken } from 'web-auth-library/google'
import type { EventHandler, EventHandlerRequest } from 'h3'

export function defineProtectedEventHandler<T extends EventHandlerRequest, D>(
  handler: EventHandler<T, D>,
): EventHandler<T, D> {
  return defineEventHandler<T>(async (event) => {
    const authorization = getHeader(event, 'Authorization') ?? ''

    if (!authorization.startsWith('Bearer ')) {
      throw createError({
        status: 403,
        statusMessage: 'Unauthorized',
      })
    }

    const idToken = authorization.substring(7, authorization.length)
    await verifyIdToken({
      idToken,
      projectId: useRuntimeConfig().firebase.projectId,
    })

    return handler(event)
  })
}
