import { eq } from 'drizzle-orm'
import { z } from 'zod'
import { events } from '~/server/db/schema'

const updateEventRequestBody = z.object({
  name: z.string()
    .min(1),
  description: z.string(),
  location: z.string(),
  badgeImage: z.string()
    .url()
    .refine(async (val) => {
      const res = await $fetch.raw(val, { method: 'GET' })
      return res.headers.get('content-type')?.includes('image')
    }, 'URL provided not a valid image'),
  startDateTime: z.string()
    .refine(val => dayjs(val).isValid(), 'Date provided not valid')
    .refine(val => dayjs(val).isAfter(dayjs()), 'Start date must be in the future'),
  endDateTime: z.string()
    .refine(val => dayjs(val).isValid()),
}).refine((val) => {
  return dayjs(val.startDateTime).isBefore(val.endDateTime)
}, {
  message: 'Event must end after it starts',
  path: ['endDateTime'],
})

export default defineProtectedEventHandler(async (event) => {
  const eventId = event.context.params!.id

  const result = await updateEventRequestBody.safeParseAsync(await readBody(event))
  if (!result.success) {
    throw createError({
      statusCode: 400,
      statusMessage: 'Bad request',
    })
  }

  const { data } = result

  const updatedEvent = await event.context.database.update(events)
    .set({
      name: data.name,
      description: data.description,
      location: data.location,
      badgeImage: data.badgeImage,
      startDateTime: data.startDateTime,
      endDateTime: data.endDateTime,
    })
    .where(eq(events.id, eventId))
    .returning()

  if (updatedEvent.length !== 1) {
    throw createError({
      statusCode: 500,
      statusMessage: 'Internal server error',
    })
  }
  return updatedEvent[0]
}, {
  restrictTo: ['exco'],
})
