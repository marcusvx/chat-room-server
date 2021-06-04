using System.Collections.Generic;
using ChatRoomService.Domain.Models;

namespace ChatRoomServer.Domain.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();
    }
}
