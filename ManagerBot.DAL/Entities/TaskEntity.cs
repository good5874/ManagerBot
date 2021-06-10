﻿using ManagerBot.DAL.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManagerBot.DAL.Entities
{
    public class TaskEntity
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public double? Cost { get; set; }

        [Required]
        public bool IsFinish { get; set; }

        public AreaEntity Area { get; set; }

        public virtual UserEntity User { get; set; }
        public virtual List<OperationEntity> Operations { get; set; }
    }
}
