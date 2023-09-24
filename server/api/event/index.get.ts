export default defineProtectedEventHandler((event) => {
  return event.context.database.query.events.findMany()
})
