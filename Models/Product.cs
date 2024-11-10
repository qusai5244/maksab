namespace Maksab.Models;
using System.ComponentModel.DataAnnotations;
public class Product
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(255)]
    public string Name { get; set; }
    [Required]
    [MaxLength(255)]
    public string NameAr { get; set; }
    public int ShopId { get; set; } 
    public Shop Shop { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public bool IsDigital { get; set; }
    public ProductStatus Status { get; set; }
    //public ProductType Type { get; set; } 
    public string ImagePath { get; set; }
}

public enum ProductType
{
    Food,
    Electronics,
    Clothing,
    Furniture,
    Other
}

public enum ProductStatus
{
    Active,
    Inactive
}
