import { z } from 'zod'
import { events } from '~/server/db/schema'

const createPostRequestBody = z.object({
    id: z.string().nonempty(),
    name: z.string().nonempty(),
    description: z.string().nonempty(),
    location: z.string().nonempty(),
    badgeImage: z.string().url(),
    startDateTime: z.string().datetime(),
    endDateTime: z.string().datetime()
})

export default defineProtectedEventHandler(async (event) => {
    if (event.context.user?.memberType !== "exco") {
        throw createError({
            status: 401,
            statusMessage: "Unauthorised"
        })
    }

    const result = await createPostRequestBody.safeParseAsync(await readBody(event))
    if (!result.success) {
        throw createError({
            status: 400,
            statusMessage: 'Bad Request',
        })
    }

    const { data } = result

    const checkDuplicate = await event.context.database.query.events.findFirst({
        where: (events, { eq }) => eq(events.id, data.id)
    })

    if (checkDuplicate !== undefined) {
        throw createError({
            status: 400,
            statusMessage: 'Event with same ID exists',
        })
    }

    const addedEvent = await event.context.database.insert(events).values(data)

    if (addedEvent.rowsAffected === 0) {
        throw createError({
            status: 500,
            statusMessage: 'Failed to insert new event',
        })
    }

    return data
})