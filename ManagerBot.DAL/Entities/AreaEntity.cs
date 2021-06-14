using ManagerBot.DAL.Entities.Abstract;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManagerBot.DAL.Entities
{
    public class AreaEntity : IConvertbleToTelegramButton
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual List<ProductCatalogEntity> ProductCatalog { get; set; }
    }
}
