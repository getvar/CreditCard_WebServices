using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tuya.CreditCard.Api.App.Contracts.Services;
using Tuya.CreditCard.Api.Common.Helpers;
using Tuya.CreditCard.Api.DTO.Models;

namespace Tuya.CreditCard.Api.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Permite registrar un nuevo usuario
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddUser(UserManage user)
        {
            return Ok(await ApiExecutionHelper.RunAsync(_userService.AddUser(user)));
        }

        /// <summary>
        /// Permite obtener los datos del usuario autorizado
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            return Ok(await ApiExecutionHelper.RunAsync(_userService.GetUser()));
        }

        /// <summary>
        /// Permite actualizar los datos del usuario autorizado
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateUser(UserEdit user)
        {
            return Ok(await ApiExecutionHelper.RunAsync(_userService.UpdateUser(user)));
        }
    }
}
