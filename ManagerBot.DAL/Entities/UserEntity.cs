using ManagerBot.DAL.Entities;
using ManagerBot.DAL.Entity.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManagerBot.DAL.Entity
{
    public class UserEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TelegramId { get; set; }

        public string FullName { get; set; }

        public UserEvent? CurrentEvent { get; set; }

        public List<TaskEntity> Tasks { get; set; }

        public List<CastomTaskEntity> CastomTasks { get; set; }

        public List<UserRolesEntity> UserRoles { get; set; }
    }
}
