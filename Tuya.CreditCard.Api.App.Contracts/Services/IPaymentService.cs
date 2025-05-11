using Tuya.CreditCard.Api.DTO.Models;

namespace Tuya.CreditCard.Api.App.Contracts.Services
{
    public interface IPaymentService
    {
        Task<TokenizedCard> TokenizeCreditCard(CardTokenData cardTokenData);
        Task<TransactionResponse> SendTransaction(TransactionSend transactionSend);
    }
}
