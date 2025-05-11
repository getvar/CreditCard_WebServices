using Tuya.CreditCard.Api.App.Contracts.Services;
using Tuya.CreditCard.Api.Common.Helpers;
using Tuya.CreditCard.Api.DTO.Models;
using static Tuya.CreditCard.Api.DTO.Models.Enums;

namespace Tuya.CreditCard.Api.App.Services
{
    public class PaymentService : IPaymentService
    {
        public async Task<TokenizedCard> TokenizeCreditCard(CardTokenData cardTokenData)
        {
            await Task.Delay(3000);
            return await Task.FromResult(new TokenizedCard()
            {
                 Token = GenericHelper.GenerateGuidWithoutHyphen(),
                 Bank = GenericHelper.GenerateRandomStringValueFromList(PaymentHelper.BANK_LIST.ToList()),
                 Franchise = GenericHelper.GenerateRandomStringValueFromList(PaymentHelper.FRANCHISE_LIST.ToList()),
            });
        }

        public async Task<TransactionResponse> SendTransaction(TransactionSend transactionSend)
        {
            await Task.Delay(5000);
            return new TransactionResponse()
            {
                ResponseMessage = "Ok",
                State = TransactionState.Ok
            };
        }
    }
}
