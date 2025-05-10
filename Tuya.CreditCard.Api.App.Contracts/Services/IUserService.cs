using Tuya.CreditCard.Api.DTO.Models;

namespace Tuya.CreditCard.Api.App.Contracts.Services
{
    public interface IUserService
    {
        Task<bool> AddUser(UserManage user);
        Task<bool> UpdateUser(UserManage user);
        Task<User> GetUser();
        Task<UserData> GetUserByUserName(string userName);
    }
}
