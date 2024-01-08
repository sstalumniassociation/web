import { z } from 'zod'
import { events } from '~/server/db/schema'

export default defineProtectedEventHandler(async (event) => {
  const createEventRequestBody = z.object({
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

  const result = await createEventRequestBody.safeParseAsync(await readBody(event))
  if (!result.success) {
    throw createError({
      statusCode: 400,
      statusMessage: 'Bad request',
      message: result.error.message,
    })
  }

  const { data } = result

  const createdEvent = await event.context.database.insert(events)
    .values({
      name: data.name,
      description: data.description,
      location: data.location,
      badgeImage: data.badgeImage,
      startDateTime: data.startDateTime,
      endDateTime: data.endDateTime,
    })
    .returning()

  if (createdEvent.length !== 1) {
    throw createError({
      statusCode: 500,
      statusMessage: 'Internal server error',
    })
  }

  return createdEvent[0]
})
