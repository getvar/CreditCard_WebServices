using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tuya.CreditCard.Api.App.Contracts.Services;
using Tuya.CreditCard.Api.Common.Constants;
using Tuya.CreditCard.Api.Common.Helpers;
using Tuya.CreditCard.Api.DTO.Models;

namespace Tuya.CreditCard.Api.App.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AuthService(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        public async Task<UserLoginResponse> LoginUser(UserLogin userData)
        {
            string baseErrorMessage = "Usuario y/o contraseña incorrectos";
            ValidateLoginData(userData, baseErrorMessage);
            var existsUser = await _userService.GetUserByUserName(userData.UserName);
            ValidateObjectHelper<UserData>.ValidateObject(existsUser, true, $"{baseErrorMessage}", new KeyNotFoundException(string.Empty));

            if (!PasswordHelper.CheckPassword(userData.Password, existsUser.Password))
                ExceptionHelper.GenerateException(baseErrorMessage, new KeyNotFoundException());

            var authClaims = new List<Claim>
            {
                new Claim(GeneralConstants.USER_NAME_CLAIM, existsUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, existsUser.Id.ToString()),
            };

            var token = GenerateAccessToken(authClaims);
            return new UserLoginResponse
            {
                UserName = existsUser.UserName,
                FullName = existsUser.FullName,
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

        private void ValidateLoginData(UserLogin userData, string baseErrorMessage)
        {
            ValidationHelper.ValidateEmptyString(userData.UserName, true, baseErrorMessage);
            ValidationHelper.ValidateEmptyString(userData.Password, true, baseErrorMessage);
        }

        private JwtSecurityToken GenerateAccessToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!));
            return new JwtSecurityToken(
                expires: DateTime.Now.AddMinutes(120),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
        }
    }
}
