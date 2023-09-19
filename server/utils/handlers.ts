// @ts-expect-error Bad types
import { verifyIdToken } from 'web-auth-library/google'
import type { UserToken } from 'web-auth-library/dist/google'
import type { EventHandler, EventHandlerRequest } from 'h3'
import type { User } from '~/server/db/schema'

declare module 'h3' {
  interface H3EventContext {
    user?: User
  }
}

export interface DefineProtectedEventHandlerOptions {
  allowUnlinkedUser: boolean // Allow users which do not have a `firebaseId` linked in database
}

export function defineProtectedEventHandler<T extends EventHandlerRequest, D>(
  handler: EventHandler<T, D>,
  options: DefineProtectedEventHandlerOptions = { allowUnlinkedUser: false },
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
    const { sub }: UserToken = await verifyIdToken({
      idToken,
      projectId: useRuntimeConfig().firebase.projectId,
    })

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

    event.context.user = user

    return handler(event)
  })
}
