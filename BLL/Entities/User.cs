using BLL.Models;

namespace BLL.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public decimal Height { get; set; }

        public User()
        {}

        public User(UserDto userDto)
        {
            Name = userDto.Name;
            Height = userDto.Height;
        }

        public void Update(UserDto userDto)
        {
            Name = userDto.Name;
            Height = userDto.Height;
        }
    }
}
