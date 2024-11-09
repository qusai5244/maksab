using Maksab.Data;
using Maksab.Dtos.Auth;
using Maksab.Helpers;
using Maksab.Helpers.MessageHandler;
using Maksab.Models;
using Maksab.Security.ACL;
using Maksab.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
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
        private readonly IPasswordHasher<User> _hasher;


        public AuthService(IConfiguration configuration, 
            IMessageHandler messageHandler,
            DataContext dataContext,
            IPasswordHasher<User> hasher)
        {
            _configuration = configuration;
            _messageHandler = messageHandler;
            _dataContext = dataContext;
            _hasher = hasher;
        }

        public async Task<ServiceResponse<LoginOutputDto>> LoginAsync(LoginInputDto input)
        {
            try
            {
                var user = await _dataContext
                             .Users
                             .Include(user => user.UsersToRoles)
                                .ThenInclude(userToRoles => userToRoles.Role)
                                    .ThenInclude(roles => roles.RolesToPermissions)
                                        .ThenInclude(rolesToPermissions => rolesToPermissions.Permission)
                             .FirstOrDefaultAsync(x => x.Email == input.Email && x.IsActive && !x.IsDeleted);

                var isPasswordMatched = _hasher.VerifyHashedPassword(user, user.Password, input.Password) != PasswordVerificationResult.Failed;

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

        public async Task<ServiceResponse> RegisterAsync(RegisterInputDto input)
        {
            try
            {
                var emailExist = await _dataContext
                             .Users
                             .AnyAsync(x => x.Email == input.Email && !x.IsDeleted);

                if (emailExist)
                {
                    return _messageHandler.GetServiceResponse<RegisterInputDto>(ErrorMessage.AlreadyExists, null, "Email Used already");
                }

                var user = new User
                {
                    FirstName = input.FirstName,
                    LastName = input.LastName,
                    Email = input.Email,
                    PhoneNumber = input.PhoneNumber, 
                    CreatedAt = DateTime.UtcNow,
                    IsEmailVerified = false,
                    IsPhoneVerified = false,
                    IsActive = true,
                    IsDeleted = false
                };

                user.Password = _hasher.HashPassword(user, input.Password);

                await _dataContext.Users.AddAsync(user);
                await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse(SuccessMessage.Created);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
