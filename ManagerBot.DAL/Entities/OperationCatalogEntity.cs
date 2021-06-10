using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManagerBot.DAL.Entities
{
    public class OperationCatalogEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Cost { get; set; }

        [Required]
        public virtual ProductCatalogEntity Product { get; set; }

        public virtual List<TaskEntity> Tasks { get; set; }
    }
}
