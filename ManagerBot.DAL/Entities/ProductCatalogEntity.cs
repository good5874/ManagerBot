using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManagerBot.DAL.Entities
{
    public class ProductCatalogEntity
    {
        [Key]
        public string ProductName { get; set; }

        public virtual List<ProductEntity> Operations { get; set; }
    }
}
