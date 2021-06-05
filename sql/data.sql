USE chat_rooms;

/*
    ROOMS
*/
INSERT INTO room(`id`, `name`)
VALUES (1, 'General');

INSERT INTO room(`id`, `name`)
VALUES (2, 'Memes');

INSERT INTO room(`id`, `name`)
VALUES (3, 'Dad Jokes');

INSERT INTO room(`id`, `name`)
VALUES (4, 'Software Development');

INSERT INTO room(`id`, `name`)
VALUES (5, 'Music');

INSERT INTO room(`id`, `name`)
VALUES (6, 'Movies');

INSERT INTO room(`id`, `name`)
VALUES (7, 'Books');

INSERT INTO room(`id`, `name`)
VALUES (8, 'Cuisine');

INSERT INTO room(`id`, `name`)
VALUES (9, 'Politics');

/*
    USERS
*/
INSERT INTO user(`id`, `name`)
VALUES (1, 'Bob');

INSERT INTO user(`id`, `name`)
VALUES (2, 'Kate');

INSERT INTO user(`id`, `name`)
VALUES (3, 'Jack');

INSERT INTO user(`id`, `name`)
VALUES (4, 'Maria');

INSERT INTO user(`id`, `name`)
VALUES (5, 'John');

INSERT INTO user(`id`, `name`)
VALUES (6, 'Jane');

/*
    EVENTS
*/
INSERT INTO event(`room_id`, `received_at`, `from_user_id`, `event_type`, `to_user_id`)
VALUES (1, '2020-08-22 07:23:48', 1, 'Enter', null);

INSERT INTO event(`room_id`, `received_at`, `from_user_id`, `event_type`, `to_user_id`)
VALUES (1, '2020-08-22 07:24:10', 1, 'Comment', null); 

INSERT INTO event_comment(`event_id`, `content`)
SELECT MAX(`id`), 'Hey, someone there???' FROM `event`;

INSERT INTO event(`room_id`, `received_at`, `from_user_id`, `event_type`, `to_user_id`)
VALUES (1, '2020-08-22 07:34:00', 2, 'Enter', null);

INSERT INTO event(`room_id`, `received_at`, `from_user_id`, `event_type`, `to_user_id`)
VALUES (1, '2020-08-22 07:36:10', 2, 'Comment', null); 

INSERT INTO event_comment(`event_id`, `content`)
SELECT MAX(`id`), 'Whats up?' FROM `event`;

INSERT INTO event(`room_id`, `received_at`, `from_user_id`, `event_type`, `to_user_id`)
VALUES (1, '2020-08-22 07:37:00', 1, 'Comment', null); 

INSERT INTO event_comment(`event_id`, `content`)
SELECT MAX(`id`), 'Hey Kate, how`s going?' FROM `event`;

INSERT INTO event(`room_id`, `received_at`, `from_user_id`, `event_type`, `to_user_id`)
VALUES (1, '2020-08-22 07:37:30', 2, 'Comment', null); 

INSERT INTO event_comment(`event_id`, `content`)
SELECT MAX(`id`), 'Good! You?' FROM `event`;

INSERT INTO event(`room_id`, `received_at`, `from_user_id`, `event_type`, `to_user_id`)
VALUES (1, '2020-08-22 07:38:43', 1, 'Comment', null); 

INSERT INTO event_comment(`event_id`, `content`)
SELECT MAX(`id`), 'superb! Did you hear the news?' FROM `event`;

INSERT INTO event(`room_id`, `received_at`, `from_user_id`, `event_type`, `to_user_id`)
VALUES (1, '2020-08-22 07:39:02', 2, 'Comment', null); 

INSERT INTO event_comment(`event_id`, `content`)
SELECT MAX(`id`), 'Nope. What news? Good ones?' FROM `event`;

INSERT INTO event(`room_id`, `received_at`, `from_user_id`, `event_type`, `to_user_id`)
VALUES (1, '2020-08-22 07:41:20', 1, 'Comment', null); 

INSERT INTO event_comment(`event_id`, `content`)
SELECT MAX(`id`), 'Yeah, great news. Sarah is coming to town next weekend. We`re planning a welcome party at Jonh`s place on Staurday night' FROM `event`;

INSERT INTO event(`room_id`, `received_at`, `from_user_id`, `event_type`, `to_user_id`)
VALUES (1, '2020-08-22 07:42:20', 2, 'HighFive', 1); 

INSERT INTO event(`room_id`, `received_at`, `from_user_id`, `event_type`, `to_user_id`)
VALUES (1, '2020-08-22 07:43:02', 2, 'Comment', null); 

INSERT INTO event_comment(`event_id`, `content`)
SELECT MAX(`id`), 'That`s great! Can I help with something? For the party I mean...' FROM `event`;

INSERT INTO event(`room_id`, `received_at`, `from_user_id`, `event_type`, `to_user_id`)
VALUES (1, '2020-08-22 07:44:00', 1, 'Comment', null); 

INSERT INTO event_comment(`event_id`, `content`)
SELECT MAX(`id`), 'Just be there and bring some beers ' FROM `event`;

INSERT INTO event(`room_id`, `received_at`, `from_user_id`, `event_type`, `to_user_id`)
VALUES (1, '2020-08-22 07:47:01', 2, 'Comment', null); 

INSERT INTO event_comment(`event_id`, `content`)
SELECT MAX(`id`), 'OK' FROM `event`;

INSERT INTO event(`room_id`, `received_at`, `from_user_id`, `event_type`, `to_user_id`)
VALUES (1, '2020-08-22 08:20:10', 3, 'Enter', null);

INSERT INTO event(`room_id`, `received_at`, `from_user_id`, `event_type`, `to_user_id`)
VALUES (1, '2020-08-22 08:21:00', 3, 'Comment', null); 

INSERT INTO event_comment(`event_id`, `content`)
SELECT MAX(`id`), 'Hi guys' FROM `event`;

INSERT INTO event(`room_id`, `received_at`, `from_user_id`, `event_type`, `to_user_id`)
VALUES (1, '2020-08-22 08:22:20', 2, 'HighFive', 3); 

INSERT INTO event(`room_id`, `received_at`, `from_user_id`, `event_type`, `to_user_id`)
VALUES (1, '2020-08-22 08:26:00', 1, 'Comment', null); 

INSERT INTO event_comment(`event_id`, `content`)
SELECT MAX(`id`), 'Hey Jack, I just told Kate about Sarah`s party. You`re coming too, right?' FROM `event`;

INSERT INTO event(`room_id`, `received_at`, `from_user_id`, `event_type`, `to_user_id`)
VALUES (1, '2020-08-22 08:32:00', 3, 'Comment', null); 

INSERT INTO event_comment(`event_id`, `content`)
SELECT MAX(`id`), 'I don`t know yet, I have that job to finish. Maybe I`ll show up a little late' FROM `event`;

INSERT INTO event(`room_id`, `received_at`, `from_user_id`, `event_type`, `to_user_id`)
VALUES (1, '2020-08-22 08:33:00', 1, 'Comment', null); 

INSERT INTO event_comment(`event_id`, `content`)
SELECT MAX(`id`), 'OK, I hope to see you there' FROM `event`;

INSERT INTO event(`room_id`, `received_at`, `from_user_id`, `event_type`, `to_user_id`)
VALUES (1, '2020-08-22 08:41:00', 4, 'Enter', null); 

INSERT INTO event(`room_id`, `received_at`, `from_user_id`, `event_type`, `to_user_id`)
VALUES (1, '2020-08-22 08:42:00', 4, 'Comment', null); 

INSERT INTO event_comment(`event_id`, `content`)
SELECT MAX(`id`), 'Hey everyone' FROM `event`;

INSERT INTO event(`room_id`, `received_at`, `from_user_id`, `event_type`, `to_user_id`)
VALUES (1, '2020-08-22 08:43:20', 2, 'HighFive', 4); 

INSERT INTO event(`room_id`, `received_at`, `from_user_id`, `event_type`, `to_user_id`)
VALUES (1, '2020-08-22 08:44:20', 3, 'HighFive', 4); 

INSERT INTO event(`room_id`, `received_at`, `from_user_id`, `event_type`, `to_user_id`)
VALUES (1, '2020-08-22 08:47:00', 1, 'Comment', null); 

INSERT INTO event_comment(`event_id`, `content`)
SELECT MAX(`id`), 'I need to go. Catch up with you later. Bye!' FROM `event`;

INSERT INTO event(`room_id`, `received_at`, `from_user_id`, `event_type`, `to_user_id`)
VALUES (1, '2020-08-22 08:48:00', 1, 'Leave', null); 