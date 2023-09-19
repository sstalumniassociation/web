import { drizzle } from 'drizzle-orm/libsql'
import type { LibSQLDatabase } from 'drizzle-orm/libsql'

import { createClient } from '@libsql/client'
import * as schema from '../db/schema'

declare module 'h3' {
  interface H3EventContext {
    database: LibSQLDatabase<typeof schema>
  }
}

// Need to keep local copy of database instances because
// requests sent during SSR do not have a copy of the
// Cloudflare request context
let database: LibSQLDatabase<typeof schema>

export default defineEventHandler((event) => {
  if (database) {
    event.context.database = database
  }
  else {
    const client = createClient({
      url: useRuntimeConfig().turso.url,
      authToken: useRuntimeConfig().turso.authToken,
    })
    database = event.context.database = drizzle(client, { schema })
  }
})
