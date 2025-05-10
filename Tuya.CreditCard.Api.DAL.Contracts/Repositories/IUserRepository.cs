using Tuya.CreditCard.Api.DAL.Contracts.Entities;

namespace Tuya.CreditCard.Api.DAL.Contracts.Repositories
{
    public interface IUserRepository : IAdd<UserEntity>, IEdit<UserEntity>, IGetById<UserEntity>
    {
        Task<UserEntity?> GetByUserName(string userName);
    }
}
