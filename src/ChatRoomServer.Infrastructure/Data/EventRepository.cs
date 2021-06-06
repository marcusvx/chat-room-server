using System;
using System.Collections.Generic;
using System.Linq;
using ChatRoomServer.Domain.Repositories;
using ChatRoomService.Domain.Models;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace ChatRoomServer.Infrastructure.Data
{
    internal class EventRepository : Repository, IEventRepository
    {
        public EventRepository(IConfiguration config) : base(config)
        {
        }

        public IEnumerable<Event> GetEvents(DateTime date, int roomId)
        {
            using (var conn = base.CreateConnection())
            {
                string query = @"
                    SELECT
                        e.id AS Id,
                        r.id AS RoomId,
                        r.name AS RoomName,
                        e.received_at AS ReceivedAt,
                        u1.id AS FromUserId,
                        u1.name AS FromUserName,
                        e.event_type AS EventType,
                        u2.id AS ToUserId,
                        u2.name AS ToUserName,
                        ec.content AS EventComment
                    FROM
                        `event` AS e
                    INNER JOIN `room` AS r ON
                        r.id = e.room_id
                    INNER JOIN `user` AS u1 ON
                        u1.id = e.from_user_id
                    LEFT JOIN `user` AS u2 ON
                        u2.id = e.to_user_id
                    LEFT JOIN `event_comment` AS ec ON
                        ec.event_id = e.id
                    WHERE
                        r.id = @RoomId
                        AND DATE(e.received_at) = @Date";

                var parameters = new DynamicParameters(new
                {
                    RoomId = roomId,
                    Date = date.ToString("yyyy-MM-dd")
                });

                return conn
                    .Query(query, parameters)
                    .Select(row => new Event(
                        row.Id,
                        new User(row.FromUserId, row.FromUserName),
                        row.ToUserId == null ? null : new User(row.ToUserId, row.ToUserName),
                        row.ReceivedAt,
                        new Room(row.RoomId, row.RoomName),
                        Enum.Parse<EventType>(row.EventType),
                        row.EventComment
                    ))
                    .AsList<Event>();
            }
        }

        public IEnumerable<EventSummary> GetHourlySummary(DateTime date, int roomId)
        {
            using (var conn = base.CreateConnection())
            {
                string query = @"
                    SELECT
                        event_type AS EventType,
                        event_hour AS EventHour,
                        count_type AS EventCount,
                        count_user AS UserCount
                    FROM
                        (
                        SELECT
                            event_type,
                            HOUR(received_at) as event_hour,
                            COUNT(event_type) as count_type,
                            COUNT(DISTINCT(to_user_id)) as count_user
                        FROM
                            event
                        WHERE
                            event_type <> 'HighFive' AND 
                            room_id = @RoomId AND
                            DATE(received_at) = @Date
                        GROUP BY
                            event_type,
                            to_user_id,
                            HOUR(received_at)
                    UNION
                        SELECT
                            event_type,
                            HOUR(received_at) as event_hour,
                            COUNT(DISTINCT(from_user_id)) as count_type,
                            COUNT(DISTINCT(to_user_id)) as count_user
                        FROM
                            event
                        WHERE
                            event_type = 'HighFive' AND 
                            room_id = @RoomId AND
                            DATE(received_at) = @Date
                        GROUP BY
                            HOUR(received_at)) AS SUB_QUERY
                    ORDER BY
                        event_hour;";


                var parameters = new DynamicParameters(new
                {
                    RoomId = roomId,
                    Date = date.ToString("yyyy-MM-dd")
                });

                return conn
                    .Query(query, parameters)
                    .Select(row => new EventSummary(
                        Enum.Parse<EventType>(row.EventType),
                        (int)row.EventHour,
                        (int)row.EventCount,
                        (int)row.UserCount))
                    .AsList<EventSummary>();
            }
        }
    }
}
