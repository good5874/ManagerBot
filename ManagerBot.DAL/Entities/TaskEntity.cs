using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManagerBot.DAL.Entities
{
    public class TaskEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AmountOperations { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public bool IsFinish { get; set; }
        [Required]
        public OperationCatalogEntity Operation { get; set; }
        public int OperationId { get; set; }
        [Required]
        public UserEntity User { get; set; }
        public int UserId { get; set; }
    }
}
