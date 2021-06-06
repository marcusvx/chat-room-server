namespace ChatRoomService.Domain.Models
{
    public class Room
    {
        public Room(uint id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public uint Id { get; private set; }

        public string Name { get; private set; }
    }
}
