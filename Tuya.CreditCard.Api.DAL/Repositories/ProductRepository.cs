using Microsoft.EntityFrameworkCore;
using Tuya.CreditCard.Api.DAL.Contracts;
using Tuya.CreditCard.Api.DAL.Contracts.Entities;
using Tuya.CreditCard.Api.DAL.Contracts.Repositories;

namespace Tuya.CreditCard.Api.DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICreditCardContext _creditCardContext;

        public ProductRepository(ICreditCardContext creditCardContext)
        {
            _creditCardContext = creditCardContext;
        }

        public async Task<List<ProductEntity>> GetAllAsync() => await _creditCardContext.Products.ToListAsync();
    }
}
