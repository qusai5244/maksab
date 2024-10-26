using Maksab.Data;
using Maksab.Dtos.Auth;
using Maksab.Helpers;
using Maksab.Helpers.MessageHandler;
using Maksab.Security.ACL;
using Maksab.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Maksab.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IMessageHandler _messageHandler;
        private readonly DataContext _dataContext;
        private readonly ILogger _logger;

        public AuthService(IConfiguration configuration, IMessageHandler messageHandler,
            DataContext dataContext)
        {
            _configuration = configuration;
            _messageHandler = messageHandler;
            _dataContext = dataContext;
        }

        public async Task<ServiceResponse<LoginOutputDto>> LoginAsync(LoginInputDto input)
        {
            try
            {
                var user = await _dataContext
                             .Users
                             .Include(x => x.UsersToRoles)
                             .FirstOrDefaultAsync(x => x.Email == input.Email && x.IsActive && !x.IsDeleted);

                var isPasswordMatched = user.Password == input.Password;

                if (user == null || !isPasswordMatched)
                {
                    return _messageHandler.GetServiceResponse<LoginOutputDto>(ErrorMessage.NotFound, null, "User");
                }

                var claims = new List<Claim>
            {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(Helpers.Constants.UserId, user.Id.ToString()),
            };

                var rolePermissions = user
                                      .UsersToRoles
                                      .SelectMany(ur => ur.Role.RolesToPermissions)
                                      .Select(rp => rp.Permission.Code)
                                      .Distinct()
                                      .ToList();

                foreach (var permission in rolePermissions)
                {
                    claims.Add(new Claim(UserPermissions.Permissions, permission.ToString()));
                }

                var claimsIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                var jwtSettings = _configuration.GetSection("Jwt");
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var tokenBody = new JwtSecurityToken(
                                 jwtSettings["Issuer"],
                                 jwtSettings["Audience"],
                                 claims,
                                 expires: DateTime.Now.AddMinutes(double.Parse(jwtSettings["ExpiryMinutes"])),
                                 signingCredentials: credentials);

                var token = new JwtSecurityTokenHandler().WriteToken(tokenBody);

                var loginOutput = new LoginOutputDto
                {
                    Token = token,
                    Permissions = rolePermissions
                };

                return _messageHandler.GetServiceResponse(SuccessMessage.Retrieved, loginOutput);
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
