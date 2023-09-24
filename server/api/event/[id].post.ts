import dayjs from 'dayjs'
import { eq } from 'drizzle-orm'
import { z } from 'zod'
import { events } from '~/server/db/schema'

const updateEventRequestBody = z.object({
  id: z.string(),
  name: z.string(),
  description: z.string(),
  location: z.string(),
  badgeImage: z.string().url(),
  startDateTime: z.string().datetime(),
  endDateTime: z.string().datetime()
})

export default defineProtectedEventHandler(async (event) => {
  if (event.context.user?.memberType !== "exco") {
    throw createError({
        status: 401,
        statusMessage: "Unauthorised"
    })
  }

  const eventId = event.context.params!.id

  const result = await updateEventRequestBody.safeParseAsync(await readBody(event))
  if (!result.success) {
      throw createError({
          status: 400,
          statusMessage: 'Bad Request',
      })
  }

  const { data } = result

  const updateEvent = await event.context.database.update(events)
    .set(data)
    .where(eq(events.id, eventId))
  
  return updateEvent
})
