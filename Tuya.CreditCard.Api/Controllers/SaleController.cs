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

        /// <summary>
        /// Permite confirmar un venta/compra. A través de este endpoint también se confirma la transacción de pago
        /// </summary>
        /// <param name="saleEntity"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CofirmSale(SaleAdd saleEntity)
        {
            return Ok(await ApiExecutionHelper.RunAsync(_saleService.ConfirmSale(saleEntity)));
        }

        /// <summary>
        /// Permite obtener el listado de ventas asociadas a un usuario autorizado
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetSales()
        {
            return Ok(await ApiExecutionHelper.RunAsync(_saleService.GetSaleListByUserId()));
        }
    }
}
