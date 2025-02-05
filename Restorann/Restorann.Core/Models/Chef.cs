﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restorann.Core.Models
{
    public class Chef : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string Designation { get; set; }
        public string? FbLink { get; set; }
        public string? InstaLink { get; set; } 
        public string? XLink { get; set; }
        public string? ImageUrl { get; set; }
        [NotMapped] 
        public IFormFile? ImageFile { get; set; }

    }
}
