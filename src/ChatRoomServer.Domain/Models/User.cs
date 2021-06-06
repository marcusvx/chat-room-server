namespace ChatRoomService.Domain.Models
{
    public class User
    {
        public User(uint id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public uint Id { get; private set; }

        public string Name { get; private set; }
    }
}
