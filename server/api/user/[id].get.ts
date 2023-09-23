// In this case, the id passed in path params is the firebaseId
// https://github.com/unjs/h3/issues/543

export default defineProtectedEventHandler(async (event) => {
  if (event.context.firebaseId !== event.context.params!.id) {
    throw createError({
      status: 403,
      statusMessage: 'Forbidden',
    })
  }

  return event.context.user
}, {
  cache: {
    maxAge: 5,
  },
})
