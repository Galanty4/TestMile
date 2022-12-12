using BLL.Entities;

namespace BLL.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Height { get; set; }

        public UserDto()
        {
        }

        public UserDto(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Height = user.Height;
        }
    }
}
