using System.ComponentModel.DataAnnotations;

namespace Maksab.Models
{
    public class User : BaseModel
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
        public DateTime? PhoneVerifiedAt { get; set; }
        public DateTime? EmailVerifiedAt { get; set; }
        public UserStatus Status { get; set; } 
        public bool IsDeleted { get; set; }
        public string ProfileImageUrl { get; set; } 
        public DateTime? LastLogin { get; set; }
        public ICollection<UserToRole> UsersToRoles { get; set; }

    }

    public enum UserType
    {
        Customer,
        Admin,
        BusinessOwner,
        Driver
    }

    public enum UserStatus
    {
        Active,
        Inactive,
    }
}
