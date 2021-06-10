using ManagerBot.DAL.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ManagerBot.DAL.Entities
{
    public class ProductEntity
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public ProductCatalogEntity Name { get; set; }

        [Required]
        public virtual List<OperationEntity> Operations { get; set; }

        [Required]
        public virtual OrderEntity Order { get; set; }
        public virtual AreaEntity Area { get; set; }
        public virtual StoreEntity Store { get; set; }
    }
}
