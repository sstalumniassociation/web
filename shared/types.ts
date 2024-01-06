import type { InferSelectModel } from 'drizzle-orm'
import type { articles, events, users } from '~/server/db/schema'

export type User = InferSelectModel<typeof users>
export type UserRestricted = Pick<User, 'id' | 'name'>
export type UserRestrictedWithAdmissionKey = UserRestricted & { admissionKey: string }

export type Event = InferSelectModel<typeof events>
export type EventWithAttendees = Omit<Event, 'startDateTime' | 'endDateTime'> & { startDateTime: number; endDateTime: number; attendees: UserRestrictedWithAdmissionKey[] }

export type Article = InferSelectModel<typeof articles>

export interface BuildInfo {
  version: string
  commit: string
  shortCommit: string
  time: number
}

declare module '@nuxt/schema' {
  interface NuxtAppConfig {
    buildInfo: BuildInfo
  }
}
