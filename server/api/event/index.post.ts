import { z } from 'zod'
import { events } from '~/server/db/schema'

const createEventRequestBody = z.object({
  id: z.string(),
  name: z.string(),
  description: z.string(),
  location: z.string(),
  badgeImage: z.string().url(),
  startDateTime: z.string().datetime(),
  endDateTime: z.string().datetime(),
})

export default defineProtectedEventHandler(async (event) => {
  const result = await createEventRequestBody.safeParseAsync(await readBody(event))
  if (!result.success) {
    throw createError({
      status: 400,
      statusMessage: 'Bad request',
    })
  }

  const { data } = result

  const addedEvent = await event.context.database.insert(events)
    .values({
      id: data.id,
      name: data.name,
      description: data.description,
      location: data.location,
      badgeImage: data.badgeImage,
      startDateTime: data.startDateTime,
      endDateTime: data.endDateTime,
    })

  if (addedEvent.rowsAffected === 0) {
    throw createError({
      status: 500,
      statusMessage: 'Internal server error',
    })
  }

  return data
}, {
  restrictTo: ['exco'],
})
