using Tuya.CreditCard.Api.DAL.Contracts.Entities;

namespace Tuya.CreditCard.Api.DAL.Contracts.Repositories
{
    public interface ISaleRepository : IAdd<SaleEntity>, IGetById<SaleEntity>, IGetAllByUserId<SaleEntity>
    {
    }
}
