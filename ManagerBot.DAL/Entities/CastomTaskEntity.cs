using System;
using System.ComponentModel.DataAnnotations;

namespace ManagerBot.DAL.Entities
{
    public class CastomTaskEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Cost { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public bool IsFinish { get; set; }

        [Required]
        public virtual UserEntity User { get; set; }
    }
}
