export default defineProtectedEventHandler((event) => {
  return event.context.database.query.articles.findMany()
}, {
  cache: {
    maxAge: 60,
  },
})
