import dayjs from 'dayjs'
import { events } from '~/server/db/schema'

export default defineProtectedEventHandler(async (event) => {
  if (event.context.user?.memberType !== "exco") {
    throw createError({
      status: 401,
      statusMessage: "Unauthorized"
    })
  }

  const result = await event.context.database.select().from(events)

  return result
})
