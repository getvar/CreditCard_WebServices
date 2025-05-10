using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tuya.CreditCard.Api.App.Contracts.Services;
using Tuya.CreditCard.Api.Common.Helpers;
using Tuya.CreditCard.Api.DTO.Models;

namespace Tuya.CreditCard.Api.Controllers
{
    [Authorize]
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserManage user)
        {
            return Ok(await ApiExecutionHelper.RunAsync(_userService.AddUser(user)));
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            return Ok(await ApiExecutionHelper.RunAsync(_userService.GetUser()));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UserManage user)
        {
            return Ok(await ApiExecutionHelper.RunAsync(_userService.UpdateUser(user)));
        }
    }
}
