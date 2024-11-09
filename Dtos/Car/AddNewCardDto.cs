using Maksab.Models;
using System.ComponentModel.DataAnnotations;

namespace Maksab.Dtos.Car
{
    public class AddNewCardDto
    {
        [Range(1, 500000)]
        public int Year { get; set; }

        [Range(0, 500000)]
        public int Mileage { get; set; }

        [MaxLength(255)]
        public string Color { get; set; }

        [Range(1, 500000)]
        public int Price { get; set; }

        [EnumDataType(typeof(FuelType))]
        public FuelType FuelType { get; set; }

        [EnumDataType(typeof(TransmissionType))]
        public TransmissionType TransmissionType { get; set; }
    }
}
