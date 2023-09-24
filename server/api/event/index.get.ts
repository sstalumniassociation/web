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
      status: 400,
      statusMessage: 'Bad request',
    })
  }

  return result.map((item) => {
    const { usersToEvents, ...data } = item

    return {
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
