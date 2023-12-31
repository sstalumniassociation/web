import * as dotenv from 'dotenv'
import type { Config } from 'drizzle-kit'

dotenv.config()

export default {
  schema: './server/db/schema.ts',
  out: './server/drizzle',
  driver: 'turso',
  dbCredentials: {
    url: process.env.TURSO_URL!,
    authToken: process.env.TURSO_AUTH_TOKEN!,
  },
} satisfies Config
