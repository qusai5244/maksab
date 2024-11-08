using Maksab.Models;
using System.ComponentModel.DataAnnotations;

namespace Maksab.Dtos.Product
{
    public class UpdateProductDto
    {
 

        [Required]
        public string Name { get; set; }

        [Required]
        public string NameAr { get; set; }


        public bool Status { get; set; } // Active/Inactive status

        public string Type { get; set; } // Shop type

        public string ImagePAth { get; set; } // Path for logo image
    }
}
