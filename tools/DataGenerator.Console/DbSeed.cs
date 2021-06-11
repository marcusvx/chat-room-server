using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ChatRoomService.Domain.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace DataGenerator.Console
{
    public class DbSeed
    {
        private readonly IConfiguration config;

        public DbSeed(IConfiguration config)
        {
            this.config = config;
        }

        public void Generate()
        {
            var rooms = CreateRooms();
            for (int days = 0; days < 10; days++)
            {
                System.Console.WriteLine($"Creating events for {days} ago");

                Parallel.ForEach(rooms, (room) =>
                {
                    var random = new Bogus.Randomizer();

                    var users = new Queue<User>(CreateUsers());
                    var activeUsers = new Queue<User>();
                    int hour = random.Number(8, 17);
                    int minute = random.Number(0, 59);
                    var baseTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, hour, minute, 0);
                    var date = new DateTimeOffset(baseTime).AddDays(-days);

                    System.Console.WriteLine($"Creating events for room {room.Name}");

                    GenerateEvents(activeUsers, users, date, room);
                });
            }
        }

        private void GenerateEvents(
            Queue<User> activeUsers,
            Queue<User> inactiveUsers,
            DateTimeOffset dateTime,
            Room room)
        {
            var random = new Bogus.Randomizer();
            int generateEvents = random.Number(1000, 1200);
            int halfEvents = generateEvents / 2;

            for (int i = 0; i < generateEvents; i++)
            {
                dateTime = dateTime.AddSeconds(random.Number(10, 30));

                string timestamp = dateTime.ToString("yyyy-MM-dd hh:mm:ss");

                if (activeUsers.Count == 0)
                {
                    EnterUser(activeUsers, inactiveUsers, room.Id, timestamp);
                    continue;
                }

                var fromUser = random.ArrayElement<User>(activeUsers.ToArray());

                // create a weighted selection of possible events
                var eventType = random.WeightedRandom<EventType>(
                    Enum.GetValues<EventType>(),
                    new[] { .50f, .30f, .15f, .5f });

                switch (eventType)
                {
                    // Half of the events should be comments
                    case EventType.Comment:
                        {
                            var commentGen = new Bogus.DataSets.Hacker();
                            CreateEvent(EventType.Comment, new
                            {
                                FromUserId = fromUser.Id,
                                Comment = random.Bool() ? commentGen.Phrase() : random.Words(random.Number(8, 12)),
                                RoomId = room.Id,
                                Date = timestamp
                            });
                            break;
                        }

                    // 30% of the random events should be user entering, until no more users are available
                    case EventType.Enter:
                        {
                            EnterUser(activeUsers, inactiveUsers, room.Id, timestamp);
                            break;
                        }

                    // 15% of the random events should be user leaving, until all users left
                    case EventType.Leave when activeUsers.Any() && i > halfEvents:
                        {
                            var leaveUser = activeUsers.Dequeue();
                            CreateEvent(EventType.Leave, new
                            {
                                FromUserId = leaveUser.Id,
                                RoomId = room.Id,
                                Date = timestamp
                            });
                            break;
                        }

                    // at least 5% of the events should be HighFives and only for half of the users
                    case EventType.HighFive when activeUsers.Count > 1 && fromUser.Id % 2 == 0:
                        {
                            var toUser = activeUsers
                                .Where(u => u.Id != fromUser.Id)
                                .OrderBy(u => new Random().Next())
                                .First();

                            CreateEvent(EventType.HighFive, new
                            {
                                FromUserId = fromUser.Id,
                                ToUserId = toUser.Id,
                                RoomId = room.Id,
                                Date = timestamp
                            });
                            break;
                        }
                }
            }
        }

        private User EnterUser(
            Queue<User> activeUsers,
            Queue<User> inactiveUsers,
            uint roomId,
            string timestamp)
        {
            if (inactiveUsers.Count == 0)
                return null;

            var enterUser = inactiveUsers.Dequeue();
            CreateEvent(EventType.Enter, new
            {
                FromUserId = enterUser.Id,
                RoomId = roomId,
                Date = timestamp
            });
            activeUsers.Enqueue(enterUser);
            return enterUser;
        }

        private void CreateEvent(EventType eventType, object data)
        {

            using var connection = Connect(config);
            switch (eventType)
            {
                case EventType.Enter:
                    connection.Execute(@"INSERT INTO event(`room_id`, `received_at`, `from_user_id`, `event_type`, `to_user_id`)
                                VALUES (@RoomId, @Date, @FromUserId, 'Enter', null);", data);
                    break;

                case EventType.Comment:
                    connection.Execute(
                        @"INSERT INTO event(`room_id`, `received_at`, `from_user_id`, `event_type`, `to_user_id`)
                              VALUES (@RoomId, @Date, @FromUserId, 'Comment', null);
                              
                              INSERT INTO event_comment(`event_id`, `content`)
                              VALUES (LAST_INSERT_ID(), @Comment); ", data);

                    break;

                case EventType.HighFive:
                    connection.Execute(@"INSERT INTO event(`room_id`, `received_at`, `from_user_id`, `event_type`, `to_user_id`)
                                VALUES (@RoomId, @Date, @FromUserId, 'HighFive', @ToUserId);", data);
                    break;

                case EventType.Leave:
                    connection.Execute(@"INSERT INTO event(`room_id`, `received_at`, `from_user_id`, `event_type`, `to_user_id`)
                                VALUES (@RoomId, @Date, @FromUserId, 'Leave', null);", data);
                    break;
            }
        }

        private IDbConnection Connect(IConfiguration config)
        {
            var connBuilder = new MySqlConnectionStringBuilder();

            connBuilder.Add("Server", config["DB_HOST"]);
            connBuilder.Add("Database", config["DB_NAME"]);
            connBuilder.Add("Uid", config["DB_USER"]);
            connBuilder.Add("Pwd", config["DB_PASSWORD"]);

            return new MySqlConnection(connBuilder.ConnectionString);
        }

        private User[] CreateUsers()
        {
            var randomName = new Bogus.DataSets.Name();
            var users = new List<User>();
            string sql = "INSERT INTO user (name) VALUES (@Name); SELECT LAST_INSERT_ID();";

            using (var connection = Connect(config))
            {
                foreach (var item in Enumerable.Range(0, 10))
                {
                    string name = randomName.FirstName();
                    uint id = connection.ExecuteScalar<uint>(sql, new { Name = name });
                    users.Add(new User(id, name));
                }
            }

            return users.ToArray();
        }

        private Room[] CreateRooms()
        {
            var rooms = new Room[] {
                new Room(1,  "General"),
                new Room(2,  "Memes"),
                new Room(3,  "Dad Jokes"),
                new Room(4,  "Software Development"),
                new Room(5,  "Music"),
                new Room(6,  "Movies"),
                new Room(7,  "Books"),
                new Room(8,  "Cuisine"),
                new Room(9,  "Politics"),
            };

            try
            {
                using var connection = Connect(config);
                connection.Open();
                connection.Execute("INSERT INTO room (id, name) VALUES (@Id, @Name)", rooms);
            }
            catch (Exception)
            {
                System.Console.WriteLine("Failed to insert rooms, ignoring since they may be already created.");
            }

            return rooms;
        }

    }
}