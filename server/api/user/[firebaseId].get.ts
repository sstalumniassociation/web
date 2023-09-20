export default defineProtectedEventHandler(async (event) => {
  if (event.context.firebaseId !== event.context.params!.firebaseId) {
    throw createError({
      status: 403,
      statusMessage: 'Forbidden',
    })
  }

  return event.context.user
})
