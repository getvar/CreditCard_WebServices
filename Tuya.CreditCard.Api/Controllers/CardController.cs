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

        [HttpPost]
        public async Task<IActionResult> AddCard(CardAdd card)
        {
            return Ok(await ApiExecutionHelper.RunAsync(_cardService.AddCard(card)));
        }

        [HttpGet]
        public async Task<IActionResult> GetCards()
        {
            return Ok(await ApiExecutionHelper.RunAsync(_cardService.GetAllCards()));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCard(CardEdit card)
        {
            return Ok(await ApiExecutionHelper.RunAsync(_cardService.UpdateCard(card)));
        }

        [HttpDelete("{cardId}")]
        public async Task<IActionResult> DeleteCard(Guid cardId)
        {
            return Ok(await ApiExecutionHelper.RunAsync(_cardService.DeleteCard(cardId)));
        }
    }
}
