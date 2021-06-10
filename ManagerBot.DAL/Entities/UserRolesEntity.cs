using ManagerBot.DAL.Entity;
using System.ComponentModel.DataAnnotations;

namespace ManagerBot.DAL.Entities
{
    public class UserRolesEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public UserEntity User { get; set; }

        [Required]
        public RoleEntity Role { get; set; }
    }
}
