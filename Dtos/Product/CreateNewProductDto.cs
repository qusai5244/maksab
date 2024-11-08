using Maksab.Models;
using System.ComponentModel.DataAnnotations;

namespace Maksab.Dtos.Product
{
    public class CreateNewProductDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string NameAr { get; set; }

        public int ShopId { get; set; } // Foreign key to Shops table (assuming it exists)

        public bool Status { get; set; } // Active/Inactive status

        public string Type { get; set; } // Shop type

        public string ImagePAth { get; set; } // Path for logo image
    }

}
