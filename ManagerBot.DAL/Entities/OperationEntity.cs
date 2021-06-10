using System.ComponentModel.DataAnnotations;

namespace ManagerBot.DAL.Entities
{
    public class OperationEntity
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public OperationCatalogEntity Name { get; set; }

        [Required]
        public double  Cost { get; set; }

        [Required]
        public bool IsFinish { get; set; }

        [Required]
        public virtual ProductEntity Product { get; set; }

        [Required]
        public virtual TaskEntity Task { get; set; }
    }
}
