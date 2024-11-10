using System.ComponentModel.DataAnnotations;

namespace Maksab.Dtos.Auth
{
    public class LoginInputDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
