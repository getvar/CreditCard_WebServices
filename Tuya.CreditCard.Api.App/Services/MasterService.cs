using Tuya.CreditCard.Api.App.Contracts.Services;
using Tuya.CreditCard.Api.Common.Extensions;
using Tuya.CreditCard.Api.DTO.Models;
using static Tuya.CreditCard.Api.DTO.Models.Enums;

namespace Tuya.CreditCard.Api.App.Services
{
    public class MasterService : IMasterService
    {
        public async Task<List<Master>> GetIdentificationTypes()
        {
            return await Task.FromResult(Enum.GetValues(typeof(IdentificationType))
                .Cast<IdentificationType>()
                .Select(e => new Master()
                {
                    Description = e.GetDisplayName(),
                    Id = (int)e,
                }).ToList());
        }

        public async Task<bool> ExistsIdentificationType(int identificationType)
        {
            var identificationTypes = await GetIdentificationTypes();
            return identificationTypes.Exists(x => x.Id.Equals(identificationType));
        }
    }
}
