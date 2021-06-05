using System.Collections.Generic;
using System.Linq;
using ChatRoomServer.Domain.Repositories;
using ChatRoomService.Domain.Models;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace ChatRoomServer.Infrastructure.Data
{
    internal class CategoryRepository : Repository, ICategoryRepository
    {
        public CategoryRepository(IConfiguration config) : base(config)
        {
        }

        public IEnumerable<Category> GetAll()
        {
            using (var conn = base.CreateConnection())
            {
                return conn.Query("SELECT Id, Name FROM Category")
                .Select(row => new Category
                {
                    Id = (int)row.Id,
                    Name = row.Name
                })
                .AsList<Category>();
            }

        }
    }
}
