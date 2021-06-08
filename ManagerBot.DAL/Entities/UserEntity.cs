using ManagerBot.DAL.Entity.Enums;

namespace ManagerBot.DAL.Entity
{
    public class UserEntity
    {
        public int Id { get; set; } 
        public int? TelegramId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }       
        public UserEvent? CurrentEvent { get; set; }
    }
}
