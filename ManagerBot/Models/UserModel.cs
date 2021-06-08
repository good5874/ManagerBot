using ManagerBot.Models.Enums;

namespace ManagerBot.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public UserEventsEnum CurrentEvent { get; set; }
    }
}
