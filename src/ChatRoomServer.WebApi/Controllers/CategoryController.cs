using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ChatRoomServer.Domain.Repositories;
using ChatRoomServer.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChatRoomServer.WebApi.Controllers
{
    [ApiController]
    [Route("categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository repository;
        private readonly IMapper mapper;
        private readonly ILogger<CategoryController> logger;

        public CategoryController(
            ILogger<CategoryController> logger,
            ICategoryRepository repository,
            IMapper mapper
        )
        {
            this.logger = logger;
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<CategoryResponse> Get()
        {
            var categories = this.repository.GetAll();
            return this.mapper.Map<CategoryResponse[]>(categories);
        }
    }
}
