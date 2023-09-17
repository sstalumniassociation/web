import { sql } from 'drizzle-orm'
import { getAuth } from 'firebase-admin/auth'

export default defineLazyEventHandler(() => eventHandler(async (event) => {
  try {
    await event.context.database.run(sql`SELECT 1;`)
    const data = await getAuth()
      .listUsers(1000)

    console.log(data.users)

    return { ok: true, now: new Date() }
  }
  catch (err) {
    throw createError({
      statusCode: 500,
      statusMessage: 'Internal Server Error',
    })
  }
}))
