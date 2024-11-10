using System.ComponentModel.DataAnnotations;

namespace Maksab.Dtos.Auth
{
    public class RegisterInputDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public FormFile ProfilePicture { get; set; } = null;
    }
}
