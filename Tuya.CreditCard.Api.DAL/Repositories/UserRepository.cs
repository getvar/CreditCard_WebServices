using Microsoft.EntityFrameworkCore;
using Tuya.CreditCard.Api.DAL.Contracts;
using Tuya.CreditCard.Api.DAL.Contracts.Entities;
using Tuya.CreditCard.Api.DAL.Contracts.Repositories;

namespace Tuya.CreditCard.Api.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ICreditCardContext _creditCardContext;

        public UserRepository(ICreditCardContext creditCardContext)
        {
            _creditCardContext = creditCardContext;
        }

        public async Task<UserEntity?> AddAsync(UserEntity entity)
        {
            _creditCardContext.Users.Add(entity);
            return await _creditCardContext.SaveChangesAsync() > 0 ? entity : null;
        }

        public async Task<UserEntity?> EditAsync(UserEntity entity)
        {
            var element = await GetByIdAsync(entity.Id);

            if (element != null)
            {
                element.Name = entity.Name;
                element.LastName = entity.LastName;
                element.Adrress = entity.Adrress;
                element.Phone = entity.Phone;
                _creditCardContext.Users.Update(element);
                return await _creditCardContext.SaveChangesAsync() > 0 ? element : null;
            }

            return null;
        }

        public async Task<UserEntity?> GetByIdAsync(Guid id) => await _creditCardContext.Users.FindAsync(id);

        public async Task<UserEntity?> GetByUserName(string userName) => await _creditCardContext.Users.FirstOrDefaultAsync(x => x.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));
    }
}
