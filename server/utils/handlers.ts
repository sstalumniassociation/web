// @ts-expect-error Bad types
import { verifyIdToken } from 'web-auth-library/google'
import { defu } from 'defu'
import type { UserToken } from 'web-auth-library/dist/google'
import type { EventHandler, EventHandlerRequest } from 'h3'
import type { CachedEventHandlerOptions } from 'nitropack'
import type { User } from '~/shared/types'

declare module 'h3' {
  interface H3EventContext {
    user?: User
    firebaseId?: string
  }
}

export interface DefineProtectedEventHandlerOptions {
  cache?: Pick<CachedEventHandlerOptions, 'maxAge'>
  allowUnlinkedUser?: boolean // Allow users which do not have a `firebaseId` linked in database
}

const defaultOptions: DefineProtectedEventHandlerOptions = {
  allowUnlinkedUser: false,
}

interface eventParameters {
  restrictTo?: Array<"exco" | "associate" | "affiliate" | "ordinary" | "revoked">
}

export function defineProtectedEventHandler<T extends EventHandlerRequest, D>(
  handler: EventHandler<T, D>,
  _options?: DefineProtectedEventHandlerOptions,
  params?: eventParameters
): EventHandler<T, D> {
  const options = defu(_options, defaultOptions)

  return defineEventHandler<T>(async (event) => {
    const authorization = getHeader(event, 'Authorization') ?? ''

    if (!authorization.startsWith('Bearer ')) {
      throw createError({
        status: 403,
        statusMessage: 'Unauthorized',
      })
    }

    const idToken = authorization.substring(7, authorization.length)
    const { sub }: UserToken = await verifyIdToken({
      idToken,
      projectId: useRuntimeConfig().firebase.projectId,
    })

    if (options.cache?.maxAge)
      setHeader(event, 'Cache-Control', `max-age=${options.cache.maxAge}`)

    if (options.allowUnlinkedUser)
      return handler(event)

    const user = await event.context.database.query.users.findFirst({
      where: (users, { eq }) => eq(users.firebaseId, sub),
    })

    if (user === null) {
      throw createError({
        status: 401,
        statusMessage: 'Unauthorized',
      })
    }

    if (user?.memberType === null || user?.memberType === undefined || params?.restrictTo?.includes(user?.memberType)) {
      throw createError({
        status: 401,
        statusMessage: 'Unauthorized',
      })
    }

    event.context.user = user
    event.context.firebaseId = sub

    return handler(event)
  })
}
