export default defineProtectedEventHandler(async (event) => {
  const eventId = event.context.params!.id

  const result = await event.context.database.query.events.findFirst({
    where: (events, { eq }) => eq(events.id, eventId),
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

  const { usersToEvents, ...data } = result

  return {
    ...data,
    attendees: usersToEvents.map(({ admissionKey, user }) => ({
      ...user,
      admissionKey,
    })),
  }
}, {
  restrictTo: ['exco'],
})
