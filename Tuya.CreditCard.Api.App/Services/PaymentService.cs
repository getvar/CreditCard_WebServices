using Tuya.CreditCard.Api.App.Contracts.Services;
using Tuya.CreditCard.Api.Common.Helpers;
using Tuya.CreditCard.Api.DTO.Models;

namespace Tuya.CreditCard.Api.App.Services
{
    public class PaymentService : IPaymentService
    {
        public Task<TokenizedCard> TokenizeCreditCard(CardAdd card)
        {
            Task.Delay(3000);
            return Task.FromResult(new TokenizedCard()
            {
                 Token = GenericHelper.GenerateGuidWithoutHyphen(),
                 Bank = GenericHelper.GenerateRandomStringValueFromList(PaymentHelper.BANK_LIST.ToList()),
                 Franchise = GenericHelper.GenerateRandomStringValueFromList(PaymentHelper.FRANCHISE_LIST.ToList()),
            });
        }
    }
}
