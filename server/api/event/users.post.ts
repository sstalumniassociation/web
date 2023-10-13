import { z } from 'zod'
import { usersToEvents } from '~/server/db/schema'

const bulkCreateEventUserRequestBody = z.array(
  z.object({
    userId: z.string().nonempty(),
    eventId: z.string().nonempty(),
    admissionKey: z.string().nonempty(),
  }),
)

export default defineProtectedEventHandler(async (event) => {
  const result = await bulkCreateEventUserRequestBody.safeParseAsync(await readBody(event))
  if (!result.success) {
    throw createError({
      statusCode: 400,
      statusMessage: 'Bad request',
    })
  }

  const { data } = result

  const createdUsers = await event.context.database.insert(usersToEvents)
    .values(
      data.map(user => ({
        userId: user.userId,
        eventId: user.eventId,
        admissionKey: user.admissionKey,
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