using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tuya.CreditCard.Api.App.Contracts.Services;
using Tuya.CreditCard.Api.Common.Helpers;
using Tuya.CreditCard.Api.DTO.Models;

namespace Tuya.CreditCard.Api.Controllers
{
    [Authorize]
    [Route("api/Sale")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpPost]
        public async Task<IActionResult> CofirmSale(SaleAdd saleEntity)
        {
            return Ok(await ApiExecutionHelper.RunAsync(_saleService.ConfirmSale(saleEntity)));
        }

        [HttpGet]
        public async Task<IActionResult> GetSales()
        {
            return Ok(await ApiExecutionHelper.RunAsync(_saleService.GetSaleListByUserId()));
        }
    }
}
