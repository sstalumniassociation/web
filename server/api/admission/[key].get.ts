// Must be public to support public pass pages
export default defineCachedEventHandler(async (event) => {
  const admission = await event.context.database.query.usersToEvents.findFirst({
    where: (usersToEvents, { eq }) => eq(usersToEvents.admissionKey, event.context.params!.key),
    with: {
      user: {
        columns: {
          name: true,
        },
      },
      event: true,
    },
  })

  if (!admission) {
    throw createError({
      statusCode: 404,
      statusMessage: 'Not found',
    })
  }

  return admission
}, {
  maxAge: 60,
})
