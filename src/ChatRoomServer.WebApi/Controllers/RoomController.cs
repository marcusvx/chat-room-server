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
        public IEnumerable<RoomResponse> GetAll()
        {
            var rooms = this.repository.GetAll();
            return this.mapper.Map<RoomResponse[]>(rooms);
        }

        [HttpGet]
        [Route("{roomId}")]
        public RoomResponse Get(uint roomId)
        {
            var room = this.repository.Get(roomId);
            return this.mapper.Map<RoomResponse>(room);
        }
    }
}
