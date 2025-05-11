using Microsoft.EntityFrameworkCore;
using Tuya.CreditCard.Api.DAL.Contracts;
using Tuya.CreditCard.Api.DAL.Contracts.Entities;
using Tuya.CreditCard.Api.DAL.Contracts.Repositories;
using static Tuya.CreditCard.Api.DTO.Models.Enums;

namespace Tuya.CreditCard.Api.DAL.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ICreditCardContext _creditCardContext;

        public TransactionRepository(ICreditCardContext creditCardContext)
        {
            _creditCardContext = creditCardContext;
        }

        public async Task<TransactionEntity?> AddAsync(TransactionEntity entity)
        {
            _creditCardContext.Transactions.Add(entity);
            return await _creditCardContext.SaveChangesAsync() > 0 ? entity : null;
        }

        public async Task<List<TransactionEntity>> GetAllByUserIdAsync(Guid userId) => await _creditCardContext.Transactions
            .Include(x => x.Sale)
            .Include(x => x.Card)
            .Where(x => x.Sale.UserId.Equals(userId) && x.State.Equals(TransactionState.Ok)).ToListAsync();
    }
}
