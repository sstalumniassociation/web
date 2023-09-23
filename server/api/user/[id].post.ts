import { eq } from 'drizzle-orm'
import { z } from 'zod'
import { users } from '~/server/db/schema'

const registerRequestBody = z.object({
  firebaseId: z.string().nonempty(),
})

export default defineProtectedEventHandler(async (event) => {
  const result = await registerRequestBody.safeParseAsync(await readBody(event))
  if (!result.success) {
    throw createError({
      status: 400,
      statusMessage: 'Bad request',
    })
  }

  const { data } = result

  const updatedUsers = await event.context.database.update(users)
    .set({
      firebaseId: data.firebaseId,
    })
    .where(
      eq(users.id, event.context.params!.id),
    )
    .returning()

  if (updatedUsers.length === 1) {
    throw createError({
      status: 500,
      statusMessage: 'Internal server error',
    })
  }

  return updatedUsers[0]
}, {
  allowUnlinkedUser: true,
})
