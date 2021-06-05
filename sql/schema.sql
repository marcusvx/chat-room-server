USE chat_rooms;

CREATE TABLE `room` (
    `id` INT UNSIGNED AUTO_INCREMENT NOT NULL,
    `name` VARCHAR(50) NOT NULL,
    PRIMARY KEY(`id`)
);

CREATE TABLE `user` (
    `id` INT UNSIGNED AUTO_INCREMENT NOT NULL,
    `name` VARCHAR(50) NOT NULL,
    PRIMARY KEY(`id`)
);

CREATE TABLE `event` (
    `id` INT UNSIGNED AUTO_INCREMENT NOT NULL,
    `room_id` INT UNSIGNED NOT NULL,
    `received_at` TIMESTAMP NOT NULL,
    `from_user_id` INT UNSIGNED NOT NULL,
    `event_type` ENUM ('Enter', 'Leave', 'Comment', 'HighFive') NOT NULL,
    `to_user_id` INT UNSIGNED,
    PRIMARY KEY(`id`),
    FOREIGN KEY (`room_id`) REFERENCES `room`(`id`),
    FOREIGN KEY (`from_user_id`) REFERENCES `user`(`id`),
    FOREIGN KEY (`to_user_id`) REFERENCES `user`(`id`)
);

CREATE INDEX index_event_received_at ON `event`(received_at);

CREATE TABLE `event_comment` (
    `event_id` INT UNSIGNED NOT NULL,
    `content` TINYTEXT NOT NULL,
    PRIMARY KEY (`event_id`),
    FOREIGN KEY (`event_id`) REFERENCES `event`(`id`)
);
