import dayjs from 'dayjs'
import { events } from '~/server/db/schema'
import { eq } from 'drizzle-orm'

export default defineProtectedEventHandler(async (event) => {
  const eventId = event.context.params!.id

  const deleteEvent = await event.context.database.delete(events)
    .where(eq(events.id, eventId))

  return deleteEvent.rowsAffected
}, undefined, { restrictTo: ["exco"] })
