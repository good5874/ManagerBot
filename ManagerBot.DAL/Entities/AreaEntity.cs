﻿using ManagerBot.DAL.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManagerBot.DAL.Entity
{
    public class AreaEntity
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual List<ProductEntity> Products { get; set; }
    }
}
