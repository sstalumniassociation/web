import { relations } from 'drizzle-orm'
import { integer, primaryKey, sqliteTable, text } from 'drizzle-orm/sqlite-core'
import { createId } from '@paralleldrive/cuid2'

// Tables

export const users = sqliteTable('users', {
  id: text('id').primaryKey().$defaultFn(() => createId()),
  memberId: text('member_id').unique(), // Member ID for tracking by SSTAA Admin
  firebaseId: text('firebase_id').unique(), // Use Firebase Auth provided ID as SSOT
  name: text('name'), // Ignore Firebase Auth provided values and force user to provide their own
  email: text('email').unique(),
  graduationYear: integer('graduation_year'),
  memberType: text('member_type', {
    enum: [
      'exco',
      'associate',
      'affiliate',
      'ordinary',
      'revoked',
    ],
  }),
})

export const events = sqliteTable('events', {
  id: text('id').primaryKey().$defaultFn(() => createId()),
  name: text('name').notNull(),
  description: text('description'),
  location: text('location'),
  badgeImage: text('badge_image'),
  startDateTime: text('start_date_time').notNull(), // ISO formatted
  endDateTime: text('end_date_time').notNull(), // ISO formatted
})

export const usersToEvents = sqliteTable('users_events', {
  userId: text('user_id').notNull().references(() => users.id, { onDelete: 'cascade', onUpdate: 'cascade' }),
  eventId: text('event_id').notNull().references(() => events.id, { onDelete: 'cascade', onUpdate: 'cascade' }),
  admissionKey: text('admission_key').notNull(),
}, t => ({
  pk: primaryKey(t.userId, t.eventId),
}))

export const locations = sqliteTable('locations', {
  id: text('id').primaryKey().$defaultFn(() => createId()),
  name: text('name').notNull(),
})

export const slots = sqliteTable('slots', {
  id: text('id').primaryKey().$defaultFn(() => createId()),
  locationId: text('location_id').references(() => locations.id, { onDelete: 'cascade', onUpdate: 'cascade' }),
  startDate: text('start_date').notNull(), // ISO formatted
  endDate: text('end_date').notNull(), // ISO formatted
  recurrenceDay: text('recurrence_day', {
    enum: [
      'monday',
      'tuesday',
      'wednesday',
      'thursday',
      'friday',
      'saturday',
      'sunday',
    ],
  }),
})

export const slotExceptions = sqliteTable('slot_exceptions', {
  id: text('id').primaryKey().$defaultFn(() => createId()),
  slotId: text('slot_id').notNull().references(() => slots.id, { onDelete: 'cascade', onUpdate: 'cascade' }),
  date: text('date').notNull(), // ISO formatted
})

export const bookings = sqliteTable('bookings', {
  id: text('id').primaryKey().$defaultFn(() => createId()),
  userId: text('user_id').notNull().references(() => users.id, { onDelete: 'cascade', onUpdate: 'cascade' }),
  slotId: text('slot_id').notNull().references(() => slots.id, { onDelete: 'cascade', onUpdate: 'cascade' }),
  statusCode: text('status', {
    enum: [
      'pending',
      'cancelled',
      'approved',
      'rejected',
    ],
  }),
})

export const articles = sqliteTable('articles', {
  id: text('id').primaryKey().$defaultFn(() => createId()),
  title: text('title').notNull(),
  description: text('description'),
  heroImageAlt: text('hero_image_alt'),
  heroImageUrl: text('hero_image_url'),
  ctaTitle: text('cta_title'),
  ctaUrl: text('cta_url'),
})

// Relations

export const usersRelations = relations(users, ({ many }) => ({
  usersToEvents: many(usersToEvents),
  bookings: many(bookings),
}))

export const eventsRelations = relations(events, ({ many }) => ({
  usersToEvents: many(usersToEvents),
}))

export const usersToEventsRelations = relations(usersToEvents, ({ one }) => ({
  user: one(users, {
    fields: [usersToEvents.userId],
    references: [users.id],
  }),
  event: one(events, {
    fields: [usersToEvents.eventId],
    references: [events.id],
  }),
}))

export const locationsRelations = relations(locations, ({ many }) => ({
  slots: many(slots),
}))

export const slotsRelations = relations(slots, ({ one, many }) => ({
  location: one(locations, {
    fields: [slots.locationId],
    references: [locations.id],
  }),
  exceptions: many(slotExceptions),
  bookings: many(bookings),
}))

export const slotExceptionsRelations = relations(slotExceptions, ({ one }) => ({
  slot: one(slots, {
    fields: [slotExceptions.slotId],
    references: [slots.id],
  }),
}))

export const bookingsRelations = relations(bookings, ({ one }) => ({
  user: one(users, {
    fields: [bookings.userId],
    references: [users.id],
  }),
  slot: one(slots, {
    fields: [bookings.slotId],
    references: [slots.id],
  }),
}))
