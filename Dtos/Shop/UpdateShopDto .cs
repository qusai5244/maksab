﻿using Maksab.Models;
using System.ComponentModel.DataAnnotations;

namespace Maksab.Dtos.Shop
{
    public class UpdateShopDto
    {
 

        [Required]
        public string Name { get; set; }

        [Required]
        public string NameAr { get; set; }


        public bool Status { get; set; } // Active/Inactive status

        public string Type { get; set; } // Shop type

        public string LogoPath { get; set; } // Path for logo image
    }
}
