﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doch.Models
{
    public class Position
    {
        [Key]
        public int PositionId { get; set; }
        [Required]
        [StringLength(50)]
        public string PositionName { get; set; }
    }
}