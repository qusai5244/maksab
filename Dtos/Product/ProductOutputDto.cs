using Maksab.Models;
using System.ComponentModel.DataAnnotations;

namespace Maksab.Dtos.Product
{
    public class ProductOutputDto
    {
        [Key]
        public int Id { get; set; }

        [Range(0, 500000)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        [Required]
        public Category Category { get; set; }
        [Required]
        public decimal Price { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }


    }
}
