DROP INDEX IF EXISTS `users_id_unique`;--> statement-breakpoint
ALTER TABLE users ADD `firebase_id` text;--> statement-breakpoint
CREATE UNIQUE INDEX `users_firebase_id_unique` ON `users` (`firebase_id`);
