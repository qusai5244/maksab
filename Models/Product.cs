using System.ComponentModel.DataAnnotations;

namespace Maksab.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public decimal Price { get; set; }
    }

    public enum Category
    {
        Electronics = 1,
        Clothing = 2,
        Home = 3,
    }
}
