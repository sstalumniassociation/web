import { z } from 'zod'
import { usersToEvents } from '~/server/db/schema'

const bulkCreateEventUserRequestBody = z.array(
  z.object({
    userId: z.string().cuid2(),
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
        eventId: event.context.params!.id,
      })),
    )
    .onConflictDoNothing()
    .returning()

  return createdUsers
}, {
  restrictTo: ['exco'],
})
