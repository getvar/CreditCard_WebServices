using Microsoft.AspNetCore.Mvc;
using Tuya.CreditCard.Api.App.Contracts.Services;
using Tuya.CreditCard.Api.Common.Helpers;
using Tuya.CreditCard.Api.DTO.Models;

namespace Tuya.CreditCard.Api.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Permite el inicio de sesión a un usuario autorizado
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login(UserLogin userData)
        {
            return Ok(await ApiExecutionHelper.RunAsync(_authService.LoginUser(userData)));
        }
    }
}
