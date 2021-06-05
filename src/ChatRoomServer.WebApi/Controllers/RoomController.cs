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
    [Route("rooms")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository repository;
        private readonly IMapper mapper;
        private readonly ILogger<RoomController> logger;

        public RoomController(
            ILogger<RoomController> logger,
            IRoomRepository repository,
            IMapper mapper
        )
        {
            this.logger = logger;
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<RoomResponse> Get()
        {
            var categories = this.repository.GetAll();
            return this.mapper.Map<RoomResponse[]>(categories);
        }
    }
}
