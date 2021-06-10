using ManagerBot.DAL.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManagerBot.DAL.Entity
{
    public class OrderEntity
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public bool IsFinish { get; set; }

        [Required]
        public virtual List<ProductEntity> Products  { get; set; }
    }
}
