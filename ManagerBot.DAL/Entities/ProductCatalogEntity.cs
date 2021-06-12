using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManagerBot.DAL.Entities
{
    public class ProductCatalogEntity
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public virtual AreaEntity Area { get; set; }

        [Required]
        public virtual List<OperationCatalogEntity> OperationCatalog { get; set; }
    }
}
