import { z } from 'zod'
import { users } from '~/server/db/schema'

const bulkCreateUserRequestBody = z.array(
  z.object({
    memberId: z.string().min(1),
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
    })
  }

  const { data } = result

  const createdUsers = await event.context.database.insert(users)
    .values(
      data.map(user => ({
        memberId: user.memberId,
        name: user.name,
        email: user.email,
        graduationYear: user.graduationYear,
        memberType: user.memberType,
      })),
    )
    .returning()

  if (createdUsers.length !== data.length) {
    throw createError({
      statusCode: 500,
      statusMessage: 'Internal server error',
    })
  }

  return createdUsers
}, {
  restrictTo: ['exco'],
})
