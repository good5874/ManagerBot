using ManagerBot.DAL.Entities;
using ManagerBot.DAL.Entity.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManagerBot.DAL.Entity
{
    public class UserEntity
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int TelegramId { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public UserEvent? CurrentEvent { get; set; }

        public virtual List<TaskEntity> Tasks  { get; set; }

        public virtual List<UserRolesEntity> UserRoles { get; set; }
    }
}
