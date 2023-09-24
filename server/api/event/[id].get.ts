import dayjs from 'dayjs'
import { usersToEvents, events } from '~/server/db/schema'
import { eq } from 'drizzle-orm'

export default defineProtectedEventHandler(async (event) => {
  const eventId = event.context.params!.id

  interface resultType {
    attendees?: {
      userId: string;
      eventId: string;
      admissionKey: string;
    }[];
    id: string;
    name: string;
    description: string;
    location: string;
    badgeImage: string;
    startDateTime: string;
    endDateTime: string;
    usersToEvents?: {
      userId: string;
      eventId: string;
      admissionKey: string;
    }[];
  }

  const result: resultType[] = await event.context.database.query.events.findMany({
    where: eq(events.id, eventId),
    with: {
      usersToEvents: {
        where: eq(usersToEvents.eventId, eventId)
      }
    }
  })
  result[0]["attendees"] = result[0]["usersToEvents"]
  delete result[0]["usersToEvents"]

  if (result === undefined) {
    throw createError({
      status: 400,
      statusMessage: "Bad Request"
    })
  }
  return result[0]
})
