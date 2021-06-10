using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManagerBot.DAL.Entities
{
    public class OperationCatalogEntity
    {
        [Key]
        public string NameOperation { get; set; }

        public virtual List<OperationEntity> Operations { get; set; }
    }
}
