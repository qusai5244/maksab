using Maksab.Dtos.Auth;
using Maksab.Helpers;

namespace Maksab.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ServiceResponse<LoginOutputDto>> LoginAsync(LoginInputDto input);
        Task<ServiceResponse> RegisterAsync(RegisterInputDto input);
    }
}
