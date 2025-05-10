using Tuya.CreditCard.Api.DAL.Contracts.Entities;

namespace Tuya.CreditCard.Api.DAL.Contracts.Repositories
{
    public interface ITransactionRepository : IAdd<TransactionEntity>, IGetAllByUserId<TransactionEntity>
    {
    }
}
