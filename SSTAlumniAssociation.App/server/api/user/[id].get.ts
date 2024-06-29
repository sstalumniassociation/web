// In this case, the id passed in path params is the firebaseId
// https://github.com/unjs/h3/issues/543

export default defineProtectedEventHandler(async (event) => {
  if (event.context.firebaseId !== event.context.params!.id) {
    throw createError({
      statusCode: 403,
      statusMessage: 'Forbidden',
    })
  }

  return event.context.appUser
}, {
  cache: {
    maxAge: 5,
  },
})
