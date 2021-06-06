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

        public IEnumerable<EventSummary> GetHourlySummary(DateTime dateTime, int roomId)
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
                            COUNT(DISTINCT(to_user_id) ) as count_user
                        FROM
                            event
                        WHERE
                            event_type <> 'HighFive' AND room_id = @RoomId
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
                            event_type = 'HighFive' AND room_id = @RoomId
                        GROUP BY
                            HOUR(received_at)) AS SUB_QUERY
                    ORDER BY
                        event_hour;
                ";


                var parameters = new DynamicParameters(new { RoomId = roomId });
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
