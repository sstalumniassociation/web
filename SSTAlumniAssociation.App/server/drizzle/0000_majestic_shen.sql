CREATE TABLE `bookings` (
	`id` text PRIMARY KEY NOT NULL,
	`user_id` text NOT NULL,
	`slot_id` text NOT NULL,
	`status` text,
	FOREIGN KEY (`user_id`) REFERENCES `users`(`id`) ON UPDATE cascade ON DELETE cascade,
	FOREIGN KEY (`slot_id`) REFERENCES `slots`(`id`) ON UPDATE cascade ON DELETE cascade
);
--> statement-breakpoint
CREATE TABLE `events` (
	`id` text PRIMARY KEY NOT NULL,
	`name` text NOT NULL,
	`description` text,
	`location` text,
	`badge_image` text,
	`start_date_time` text NOT NULL,
	`end_date_time` text NOT NULL
);
--> statement-breakpoint
CREATE TABLE `locations` (
	`id` text PRIMARY KEY NOT NULL,
	`name` text NOT NULL
);
--> statement-breakpoint
CREATE TABLE `slot_exceptions` (
	`id` text PRIMARY KEY NOT NULL,
	`slot_id` text NOT NULL,
	`date` text NOT NULL,
	FOREIGN KEY (`slot_id`) REFERENCES `slots`(`id`) ON UPDATE cascade ON DELETE cascade
);
--> statement-breakpoint
CREATE TABLE `slots` (
	`id` text PRIMARY KEY NOT NULL,
	`location_id` text,
	`start_date` text NOT NULL,
	`end_date` text NOT NULL,
	`recurrence_day` text,
	FOREIGN KEY (`location_id`) REFERENCES `locations`(`id`) ON UPDATE cascade ON DELETE cascade
);
--> statement-breakpoint
CREATE TABLE `users` (
	`id` text PRIMARY KEY NOT NULL,
	`name` text,
	`member_id` text,
	`graduation_year` integer,
	`member_type` text
);
--> statement-breakpoint
CREATE TABLE `users_events` (
	`user_id` text NOT NULL,
	`event_id` text NOT NULL,
	PRIMARY KEY(`event_id`, `user_id`),
	FOREIGN KEY (`user_id`) REFERENCES `users`(`id`) ON UPDATE cascade ON DELETE cascade,
	FOREIGN KEY (`event_id`) REFERENCES `events`(`id`) ON UPDATE cascade ON DELETE cascade
);
--> statement-breakpoint
CREATE UNIQUE INDEX `users_member_id_unique` ON `users` (`member_id`);
