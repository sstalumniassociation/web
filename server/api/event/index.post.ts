import { z } from 'zod'
import { events } from '~/server/db/schema'

const createEventRequestBody = z.object({
    id: z.string(),
    name: z.string(),
    description: z.string(),
    location: z.string(),
    badgeImage: z.string().url(),
    startDateTime: z.string().datetime(),
    endDateTime: z.string().datetime()
}).strict()

export default defineProtectedEventHandler(async (event) => {
    const result = await createEventRequestBody.safeParseAsync(await readBody(event))
    if (!result.success) {
        throw createError({
            status: 400,
            statusMessage: 'Bad Request',
        })
    }

    const { data } = result

    const addedEvent = await event.context.database.insert(events).values(data)

    if (addedEvent.rowsAffected === 0) {
        throw createError({
            status: 500,
            statusMessage: 'Failed to insert new event',
        })
    }

    return data
}, undefined, { restrictTo: ["exco"] })