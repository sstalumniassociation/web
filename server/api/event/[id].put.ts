import { eq } from 'drizzle-orm'
import { z } from 'zod'
import { events } from '~/server/db/schema'

const updateEventRequestBody = z.object({
  name: z.string().optional(),
  description: z.string().optional(),
  location: z.string().optional(),
  badgeImage: z.string().url().optional(),
  startDateTime: z.string().datetime().optional(),
  endDateTime: z.string().datetime().optional(),
})

export default defineProtectedEventHandler(async (event) => {
  const eventId = event.context.params!.id

  const result = await updateEventRequestBody.safeParseAsync(await readBody(event))
  if (!result.success) {
    console.log(result.error)
    throw createError({
      statusCode: 400,
      statusMessage: 'Bad request',
    })
  }

  const { data } = result

  const updatedEvent = await event.context.database.update(events)
    .set(data)
    .where(eq(events.id, eventId))
    .returning()

  if (updatedEvent.length !== 1) {
    throw createError({
      statusCode: 500,
      statusMessage: 'Internal server error',
    })
  }
  return updatedEvent[0]
})