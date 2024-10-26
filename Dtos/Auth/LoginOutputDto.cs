
namespace Maksab.Dtos.Auth
{
    public class LoginOutputDto
    {
        public string Token { get; set; }
        public List<int> Permissions { get; set; }
    }
}
