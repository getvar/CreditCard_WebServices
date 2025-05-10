using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Tuya.CreditCard.Api.DAL.Contracts.Entities;

namespace Tuya.CreditCard.Api.DAL.Contracts
{
    public interface ICreditCardContext
    {
        DbSet<UserEntity> Users { get; set; }
        DbSet<CardEntity> Cards { get; set; }
        DbSet<ProductEntity> Products { get; set; }
        DbSet<SaleEntity> Sales { get; set; }
        DbSet<SaleDetailEntity> SaleDetails { get; set; }
        DbSet<TransactionEntity> Transactions { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DatabaseFacade DataBase { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        void RemoveRange(IEnumerable<object> entities);
        EntityEntry Update(object entity);
    }
}
