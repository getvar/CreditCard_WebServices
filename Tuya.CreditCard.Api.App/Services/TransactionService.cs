using Tuya.CreditCard.Api.App.Contracts.Services;
using Tuya.CreditCard.Api.Common.Helpers;
using Tuya.CreditCard.Api.DTO.Models;
using static Tuya.CreditCard.Api.DTO.Models.Enums;

namespace Tuya.CreditCard.Api.App.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IPaymentService _paymentService;

        public TransactionService(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task<Transaction?> ConfirmTransaction(TransactionSend transactionSend)
        {
            transactionSend.TransactionReference = GenericHelper.GenerateGuidWithoutHyphen();
            var transaction = await _paymentService.SendTransaction(transactionSend);

            if (transaction != null && transaction.State.Equals(TransactionState.Ok))
            {
                return new Transaction()
                {
                    TransactionReference = transactionSend.TransactionReference,
                    Value = transactionSend.Value.ToString(),
                    State = transaction.State.ToString(),
                    ResponseMessage = transaction.ResponseMessage
                };
            }

            return null;
        }
    }
}
