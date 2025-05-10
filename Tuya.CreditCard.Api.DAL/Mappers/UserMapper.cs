using AutoMapper;
using Tuya.CreditCard.Api.Common.Helpers;
using Tuya.CreditCard.Api.DAL.Contracts.Entities;
using Tuya.CreditCard.Api.DTO.Models;

namespace Tuya.CreditCard.Api.DAL.Mappers
{
    public static class UserMapper
    {
        public static UserEntity MapAdd(UserManage user, IMapper mapper)
        {
            var entity = mapper.Map<UserEntity>(user);
            entity.Id = Guid.NewGuid();
            entity.RegistrationDate = DateTime.UtcNow;
            entity.Password = PasswordHelper.GetPasswordHash(user.Password);
            return entity;
        }

        public static UserEntity MapUpdate(UserManage user, IMapper mapper)
        {
            var entity = mapper.Map<UserEntity>(user);
            entity.UpdateDate = DateTime.UtcNow;
            return entity;
        }
    }
}
