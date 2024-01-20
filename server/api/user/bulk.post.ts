import { eq, or } from 'drizzle-orm'
import { z } from 'zod'
import { users } from '~/server/db/schema'

const bulkCreateUserRequestBody = z.array(
  z.object({
    name: z.string().min(1),
    email: z.string().email(),
    graduationYear: z.number().int().min(2011),
    memberType: z.enum([
      'exco',
      'associate',
      'affiliate',
      'ordinary',
      'revoked',
    ]),
  }),
)

export default defineProtectedEventHandler(async (event) => {
  const result = await bulkCreateUserRequestBody.safeParseAsync(await readBody(event))
  if (!result.success) {
    throw createError({
      statusCode: 400,
      statusMessage: 'Bad request',
      message: result.error.message,
    })
  }

  const { data } = result

  await event.context.database.insert(users)
    .values(
      data.map(user => ({
        name: user.name,
        email: user.email,
        graduationYear: user.graduationYear,
        memberType: user.memberType,
      })),
    )
    .onConflictDoNothing()
    .returning()

  const query = or(...data.map(u => eq(users.email, u.email)))

  return await event.context.database.select().from(users).where(query)
}, {
  restrictTo: ['exco'],
})
