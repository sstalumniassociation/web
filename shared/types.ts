import type { InferSelectModel } from 'drizzle-orm'
import type { events, news, users } from '~/server/db/schema'

export type User = InferSelectModel<typeof users>
export type UserRestricted = Pick<User, 'id' | 'name'>
export type UserRestrictedWithAdmissionKey = UserRestricted & { admissionKey: string }

export type Event = InferSelectModel<typeof events>
export type EventWithAttendees = Event & { attendees: UserRestrictedWithAdmissionKey[] }

export type NewsArticle = InferSelectModel<typeof news>

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
