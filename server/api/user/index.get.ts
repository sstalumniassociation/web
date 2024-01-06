export default defineProtectedEventHandler((event) => {
  return event.context.database.query.users.findMany({
    columns: {
      id: true,
      memberId: true,
      memberType: true,
      name: true,
      email: true,
      graduationYear: true,
    },
  })
}, {
  cache: {
    maxAge: 60,
  },
  restrictTo: ['exco'],
})
