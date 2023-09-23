import dayjs from 'dayjs'
import { events } from '~/server/db/schema'
import { eq } from 'drizzle-orm'

export default defineProtectedEventHandler(async (event) => {
  const eventId = event.context.params!.id

  if (event.context.user?.memberType !== "exco") {
    throw createError({
      status: 401,
      statusMessage: "Unauthorized"
    })
  }

  const checkForEvent = await event.context.database.query.events.findFirst({
    where: (events, { eq }) => eq(events.id, eventId)
  })

  if (checkForEvent === undefined) {
    throw createError({
      status: 400,
      statusMessage: 'Event with ID does not exist',
    }) 
  }

  const deleteEvent = await event.context.database.delete(events)
    .where(eq(events.id, eventId))

  return deleteEvent.rowsAffected
})
