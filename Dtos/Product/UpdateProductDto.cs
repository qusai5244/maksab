using Maksab.Models;
using System.ComponentModel.DataAnnotations;

namespace Maksab.Dtos.Product
{
    public class UpdateProductDto
    {
        [Key]
        public int Id { get; set; }

        [Range(0, 500000)]
        public int Name { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        [Required]
        public Category Category { get; set; }
        [Required]
        public decimal Price { get; set; }

    }
}
