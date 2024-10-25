using System.ComponentModel.DataAnnotations;

namespace Maksab.Models
{
    public class User
    {
        [Required]
        [MaxLength(255)]
        public string FirstName { get; set; } 
        [Required]
        [MaxLength(255)]
        public string LastName { get; set; } 
        [Required]
        [Phone]
        [MaxLength(50)]
        public string PhoneNumber { get; set; }  
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; } 
        public string Password { get; set; }  
        public bool IsPhoneVerified { get; set; }  
        public DateTime PhoneVerifiedAt { get; set; }
        public bool IsEmailVerified { get; set; }
        public DateTime EmailVerifiedAt { get; set; }
        public bool IsActive { get; set; } = true;  
        public bool IsDeleted { get; set; } = false;  
        public string ProfileImageUrl { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? LastLogin { get; set; }
        public ICollection<UserToRole> UsersToRoles { get; set; }

    }

    public enum UserType
    {
        Customer,
        Admin,
        RestaurantOwner,
        DeliveryDriver
    }
}
