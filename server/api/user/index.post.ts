import { z } from 'zod'
import { users } from '~/server/db/schema'

const createUserRequestBody = z.object({
  memberId: z.string().nonempty(),
  name: z.string().nonempty(),
  email: z.string().email(),
  graduationYear: z.number().int().min(2011),
  memberType: z.enum([
    'exco',
    'associate',
    'affiliate',
    'ordinary',
    'revoked',
  ]),
})

export default defineProtectedEventHandler(async (event) => {
  const result = await createUserRequestBody.safeParseAsync(await readBody(event))
  if (!result.success) {
    throw createError({
      statusCode: 400,
      statusMessage: 'Bad request',
    })
  }

  const { data } = result

  const createdUser = await event.context.database.insert(users)
    .values({
      memberId: data.memberId,
      name: data.name,
      email: data.email,
      graduationYear: data.graduationYear,
      memberType: data.memberType,
    })
    .returning()

  if (createdUser.length !== 1) {
    throw createError({
      statusCode: 500,
      statusMessage: 'Internal server error',
    })
  }

  return createdUser[0]
}, {
  restrictTo: ['exco'],
})
