namespace Maksab.Models;
using System;
using System.ComponentModel.DataAnnotations;
public class Product
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string NameAr { get; set; }

    public int ShopId { get; set; } // Foreign key to Shop table (assuming it exists)
    public int UserId { get; set; } // Foreign key to Users table (assuming it exists)


    public bool Status { get; set; } // Active/Inactive status

    public string Type { get; set; } // Shop type

    public string ImagePAth { get; set; } // Product for logo image
}
