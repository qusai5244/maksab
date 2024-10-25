namespace Maksab.Models;
using System;
using System.ComponentModel.DataAnnotations;
public class Car
{
    [Key]
    public int Id { get; set; }

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

public enum FuelType
{
    Gasoline = 1,
    Diesel = 2,
    Electric = 3,
    Hybrid = 4,
    Hydrogen = 5
}

public enum TransmissionType
{
    Automatic = 1,
    Manual = 2,
    SemiAutomatic = 3,
    CVT = 4
}
