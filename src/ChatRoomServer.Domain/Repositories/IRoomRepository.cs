using System.Collections.Generic;
using ChatRoomService.Domain.Models;

namespace ChatRoomServer.Domain.Repositories
{
    public interface IRoomRepository
    {
        IEnumerable<Room> GetAll();

        Room Get(uint roomId);
    }
}
