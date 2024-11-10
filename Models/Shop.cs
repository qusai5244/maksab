namespace Maksab.Models;
using System;
using System.ComponentModel.DataAnnotations;
public class Shop
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(255)]
    public string Name { get; set; }
    [Required]
    public string NameAr { get; set; }
    public int UserId { get; set; }
    public bool Status { get; set; } 
    public string Type { get; set; } 
    public string LogoPath { get; set; } 
}
