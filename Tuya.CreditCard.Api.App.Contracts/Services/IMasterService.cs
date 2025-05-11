using Tuya.CreditCard.Api.DTO.Models;

namespace Tuya.CreditCard.Api.App.Contracts.Services
{
    public interface IMasterService
    {
        Task<List<Master>> GetIdentificationTypes();
        Task<bool> ExistsIdentificationType(int identificationType);
    }
}
