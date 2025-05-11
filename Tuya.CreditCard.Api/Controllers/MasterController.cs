using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tuya.CreditCard.Api.App.Contracts.Services;
using Tuya.CreditCard.Api.Common.Helpers;

namespace Tuya.CreditCard.Api.Controllers
{
    [Route("api/Master")]
    [ApiController]
    public class MasterController : ControllerBase
    {
        private readonly IMasterService _masterService;

        public MasterController(IMasterService masterService)
        {
            _masterService = masterService;
        }

        [HttpGet("GetIdentificationTypes")]
        public async Task<IActionResult> GetIdentificationTypes()
        {
            return Ok(await ApiExecutionHelper.RunAsync(_masterService.GetIdentificationTypes()));
        }
    }
}
