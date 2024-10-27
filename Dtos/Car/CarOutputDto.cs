using Maksab.Models;

namespace Maksab.Dtos.Product
{
    public class CarOutputDto
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int Mileage { get; set; }
        public string Color { get; set; }
        public int Price { get; set; }
        public FuelType FuelType { get; set; }
        public TransmissionType TransmissionType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
