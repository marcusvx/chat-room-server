using System.Collections.Generic;
using ChatRoomServer.Domain.Repositories;
using ChatRoomService.Domain.Models;

namespace ChatRoomServer.Infrastructure.Data
{
    internal class CategoryRepository : ICategoryRepository
    {
        public IEnumerable<Category> GetAll()
        {
            return new []
            {
                new Category { Id = 1, Name = "Friendship" },
                new Category { Id = 2, Name = "Music" }
            };
        }
    }
}
