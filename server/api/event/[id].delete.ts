import { eq } from 'drizzle-orm'
import { events } from '~/server/db/schema'

export default defineProtectedEventHandler(async (event) => {
  const eventId = event.context.params!.id

  await event.context.database.delete(events)
    .where(eq(events.id, eventId))

  return sendNoContent(event)
}, {
  restrictTo: ['exco'],
})
