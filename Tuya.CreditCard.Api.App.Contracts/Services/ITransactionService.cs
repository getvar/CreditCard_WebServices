using Tuya.CreditCard.Api.DTO.Models;

namespace Tuya.CreditCard.Api.App.Contracts.Services
{
    public interface ITransactionService
    {
        Task<TransactionAdd?> ConfirmTransaction(TransactionPaymentAdd transactionEntity);
        Task<List<Transaction>> GetConfirmTransactions();
    }
}
