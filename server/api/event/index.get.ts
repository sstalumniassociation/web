import dayjs from 'dayjs'

export default defineProtectedEventHandler(async (event) => {
  const result = await event.context.database.query.events.findMany({
    with: {
      usersToEvents: {
        with: {
          user: {
            columns: {
              id: true,
              name: true,
            },
          },
        },
        columns: {
          admissionKey: true,
        },
      },
    },
  })

  if (!result) {
    throw createError({
      statusCode: 400,
      statusMessage: 'Bad request',
    })
  }

  return result.map((item) => {
    const { usersToEvents, startDateTime, endDateTime, ...data } = item

    return {
      startDateTime: dayjs(startDateTime).unix(),
      endDateTime: dayjs(endDateTime).unix(),
      ...data,
      attendees: usersToEvents.map(({ admissionKey, user }) => ({
        ...user,
        admissionKey,
      })),
    }
  })
}, {
  restrictTo: ['exco'],
})
