export default defineProtectedEventHandler(async (event) => {
  if (event.context.firebaseId !== event.context.params!.id) {
    throw createError({
      status: 403,
      statusMessage: 'Forbidden',
    })
  }

  return event.context.user
})
