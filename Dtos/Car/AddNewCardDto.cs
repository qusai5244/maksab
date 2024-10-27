using Maksab.Models;
using System.ComponentModel.DataAnnotations;

namespace Maksab.Dtos.Product
{
    public class AddNewProductdDto
    {
        [Required]
        public int Year { get; set; }

        [Range(0, 500000)]
        public int Mileage { get; set; }

        [MaxLength(255)]
        public string Color { get; set; }

        [Required]
        public int Price { get; set; }
        [Required]
        public FuelType FuelType { get; set; }

        [Required]
        public TransmissionType TransmissionType { get; set; }
    }
}
