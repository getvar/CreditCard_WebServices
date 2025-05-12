using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tuya.CreditCard.Api.App.Contracts.Services;
using Tuya.CreditCard.Api.Common.Helpers;
using Tuya.CreditCard.Api.DTO.Models;

namespace Tuya.CreditCard.Api.Controllers
{
    [Authorize]
    [Route("api/Card")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        /// <summary>
        /// Permite crear una tarjeta a un usuario autorizado
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddCard(CardAdd card)
        {
            return Ok(await ApiExecutionHelper.RunAsync(_cardService.AddCard(card)));
        }

        /// <summary>
        /// Permite consultar las tarjetas ACTIVAS de un usuario autorizado
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetCards()
        {
            return Ok(await ApiExecutionHelper.RunAsync(_cardService.GetAllCards()));
        }

        /// <summary>
        /// Permite actualizar una tarjeta ACTIVA a un usuario autorizado
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateCard(CardEdit card)
        {
            return Ok(await ApiExecutionHelper.RunAsync(_cardService.UpdateCard(card)));
        }

        /// <summary>
        /// Permite eliminar una tarjeta a un usuario autorizado
        /// </summary>
        /// <param name="cardId"></param>
        /// <returns></returns>
        [HttpDelete("{cardId}")]
        public async Task<IActionResult> DeleteCard(Guid cardId)
        {
            return Ok(await ApiExecutionHelper.RunAsync(_cardService.DeleteCard(cardId)));
        }
    }
}
