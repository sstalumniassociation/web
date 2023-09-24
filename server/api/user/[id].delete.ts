import { eq } from 'drizzle-orm'
import { users } from '~/server/db/schema'

export default defineProtectedEventHandler(async (event) => {
  if (event.context.user!.id !== event.context.params!.id) {
    throw createError({
      status: 403,
      statusMessage: 'Forbidden',
    })
  }

  await event.context.database.update(users)
    .set({
      firebaseId: null,
    })
    .where(
      eq(users.id, event.context.params!.id),
    )

  return sendNoContent(event)
})
