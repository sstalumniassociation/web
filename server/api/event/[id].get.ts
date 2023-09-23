import dayjs from 'dayjs'
import { usersToEvents, events } from '~/server/db/schema'
import { eq } from 'drizzle-orm'

export default defineProtectedEventHandler(async (event) => {
  const eventId = event.context.params!.id

  const result = await event.context.database.select().from(events)
    .where(eq(events.id, eventId))
    .leftJoin(usersToEvents, eq(events.id, usersToEvents.eventId))

  if (result === undefined || result[0]["users_events"] === null) {
    throw createError({
      status: 400,
      statusMessage: "Bad Request"
    })
  }

  return {
    ...result[0]["events"],
    attendees: result[0]["users_events"]
  }
})
