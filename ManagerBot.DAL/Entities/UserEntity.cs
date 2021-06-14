using ManagerBot.DAL.Entities.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManagerBot.DAL.Entities
{
    public class UserEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TelegramId { get; set; }

        public string FullName { get; set; }

        public double Salary { get; set; }

        public UserEvent? CurrentEvent { get; set; }
        public int CurrentAreaId { get; set; }
        public AreaEntity CurrentArea { get; set; }
        public int CurrentProductId { get; set; }
        public ProductCatalogEntity CurrentProduct { get; set; }
        public int CurrentOperationId { get; set; }
        public OperationCatalogEntity CurrentOperation { get; set; }
        public List<TaskEntity> Tasks { get; set; }

        public List<CastomTaskEntity> CastomTasks { get; set; }

        public List<UserRolesEntity> UserRoles { get; set; }
    }
}
