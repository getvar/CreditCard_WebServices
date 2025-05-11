using Tuya.CreditCard.Api.DTO.Models;

namespace Tuya.CreditCard.Api.App.Contracts.Services
{
    public interface IPaymentService
    {
        Task<TokenizedCard> TokenizeCreditCard(CardAdd card);
    }
}
