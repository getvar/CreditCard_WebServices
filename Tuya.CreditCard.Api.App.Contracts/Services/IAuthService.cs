using Tuya.CreditCard.Api.DTO.Models;

namespace Tuya.CreditCard.Api.App.Contracts.Services
{
    public interface IAuthService
    {
        Task<User> LoginUser(UserLogin userData);
    }
}
