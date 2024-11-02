using Maksab.Models;
using System.ComponentModel.DataAnnotations;

namespace Maksab.Dtos.Shop
{
    public class CreateNewShopDto
    {
        [Key]
        public int ShopId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string NameAr { get; set; }

        public int UserId { get; set; } // Foreign key to Users table (assuming it exists)

        public bool Status { get; set; } // Active/Inactive status

        public string Type { get; set; } // Shop type

        public string LogoPath { get; set; } // Path for logo image
    }

}
