API Creation Guide
Follow these steps to create a new API in the project:

1- create model and add it to dataContext

2- create migration for the new model

3- add model to database

4- create new service and interface for the model 

5- create dtos and enums if required

6 - create new controller


-------------------------------------------------------
product class example 

public class Product
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [MaxLength(500)]
    public string Description { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public Category ProductCategory { get; set; }

    [Required]
    public Status ProductStatus { get; set; }

    public int StockQuantity { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
public enum Category
{
    Electronics,
    Clothing,
    HomeGoods,
    Books,
    Beauty,
    Sports,
    Automotive
}

public enum Status
{
    InStock,
    OutOfStock,
    PreOrder,
    Discontinued
}
