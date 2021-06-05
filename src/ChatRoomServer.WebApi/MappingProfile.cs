using AutoMapper;
using ChatRoomServer.WebApi.Models;
using ChatRoomService.Domain.Models;

namespace ChatRoomService.WebApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Room, RoomResponse>();
        }
    }
}
