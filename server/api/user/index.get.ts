export default defineProtectedEventHandler((event) => {
  return event.context.database.query.users.findMany()
}, {
  cache: {
    maxAge: 60,
  },
  restrictTo: ['exco'],
})
