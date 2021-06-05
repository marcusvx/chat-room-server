using System.Collections.Generic;
using System.Linq;
using ChatRoomServer.Domain.Repositories;
using ChatRoomService.Domain.Models;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace ChatRoomServer.Infrastructure.Data
{
    internal class RoomRepository : Repository, IRoomRepository
    {
        public RoomRepository(IConfiguration config) : base(config)
        {
        }

        public IEnumerable<Room> GetAll()
        {
            using (var conn = base.CreateConnection())
            {
                return conn
                    .Query("SELECT id as Id, name as Name FROM room")
                    .Select(row => new Room
                    {
                        Id = row.Id,
                        Name = row.Name
                    })
                    .AsList<Room>();
            }

        }
    }
}