import { eq } from 'drizzle-orm'
import { z } from 'zod'
import { users } from '~/server/db/schema'

const registerRequestBody = z.object({
  firebaseId: z.string().nonempty(),
  email: z.string().email().optional(),
  id: z.string().cuid2().optional(),
}).refine(val => !val.email && !val.id, 'Either `email` or `id` must be provided.')

export default defineProtectedEventHandler(async (event) => {
  const result = await registerRequestBody.safeParseAsync(await readBody(event))
  if (!result.success) {
    throw createError({
      status: 400,
      statusMessage: 'Bad request',
    })
  }

  const { data } = result

  const query = data.id ? eq(users.id, data.id) : eq(users.email, data.email!)

  const user = await event.context.database.update(users)
    .set({
      firebaseId: data.firebaseId,
    })
    .where(query)
    .returning()

  return user
})
