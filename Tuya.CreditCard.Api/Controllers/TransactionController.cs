using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tuya.CreditCard.Api.App.Contracts.Services;
using Tuya.CreditCard.Api.Common.Helpers;

namespace Tuya.CreditCard.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        /// <summary>
        /// Permite obtener el listado de transacciones asociadas a un usuario autorizado
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetTransactions()
        {
            return Ok(await ApiExecutionHelper.RunAsync(_transactionService.GetConfirmTransactions()));
        }
    }
}
