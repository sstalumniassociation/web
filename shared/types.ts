import type { InferSelectModel } from 'drizzle-orm'
import type { events, users } from '~/server/db/schema'

export type User = InferSelectModel<typeof users>
export type UserRestricted = Pick<User, 'id' | 'name'>
export type UserRestrictedWithAdmissionKey = UserRestricted & { admissionKey: string }

export type Event = InferSelectModel<typeof events>
export type EventWithAttendees = Event & { attendees: UserRestrictedWithAdmissionKey[] }
