import { z } from 'zod'

const verifyRequestBody = z.object({
  email: z.string().email(),
})

export default defineEventHandler(async (event) => {
  const result = await verifyRequestBody.safeParseAsync(await readBody(event))
  if (!result.success) {
    throw createError({
      status: 400,
      statusMessage: 'Bad request',
    })
  }

  const { data } = result

  const user = await event.context.database.query.users.findFirst({
    where: (users, { eq }) => eq(users.email, data.email),
  })

  if (!user) {
    throw createError({
      status: 404,
      statusMessage: 'Not found',
    })
  }

  return {
    id: user?.id,
    linked: !!user.firebaseId,
  }
})
