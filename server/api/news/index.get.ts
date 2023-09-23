export default defineProtectedEventHandler((event) => {
  return event.context.database.query.news.findMany()
}, {
  cache: {
    maxAge: 60,
  },
})
