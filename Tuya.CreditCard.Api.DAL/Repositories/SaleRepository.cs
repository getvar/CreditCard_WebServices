using Microsoft.EntityFrameworkCore;
using Tuya.CreditCard.Api.DAL.Contracts;
using Tuya.CreditCard.Api.DAL.Contracts.Entities;
using Tuya.CreditCard.Api.DAL.Contracts.Repositories;

namespace Tuya.CreditCard.Api.DAL.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly ICreditCardContext _creditCardContext;

        public SaleRepository(ICreditCardContext creditCardContext)
        {
            _creditCardContext = creditCardContext;
        }

        public async Task<SaleEntity?> AddAsync(SaleEntity entity)
        {
            _creditCardContext.Sales.Add(entity);
            return await _creditCardContext.SaveChangesAsync() > 0 ? entity : null;
        }

        public async Task<SaleEntity?> GetByIdAsync(Guid id) => await _creditCardContext.Sales
            .Include(x => x.SaleDetails)
            .Include(x => x.Transactions)
            .FirstOrDefaultAsync(x => x.Id.Equals(id));

        public async Task<List<SaleEntity>> GetAllByUserIdAsync(Guid userId) => await _creditCardContext.Sales
            .Include(x => x.SaleDetails).ThenInclude(x => x.Product)
            .Include(x => x.Transactions).ThenInclude(x => x.Card)
            .Where(x => x.UserId.Equals(userId)).ToListAsync();
    }
}
