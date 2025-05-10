using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Tuya.CreditCard.Api.Common.Constants;
using Tuya.CreditCard.Api.Common.Contracts;

namespace Tuya.CreditCard.Api.Common.Services
{
    public class ApiAccessorUserData : IApiAccessorUserData
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApiAccessorUserData(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid GetUserId()
        {
            var identity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var claim = identity.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.NameIdentifier));

                if (claim != null)
                {
                    try
                    {
                        return Guid.Parse(claim.Value);
                    }
                    catch
                    {
                        return Guid.Empty;
                    }
                }
            }

            return Guid.Empty;
        }

        public string GetUserName()
        {
            string response = string.Empty;
            var identity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var claim = identity.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name));

                if (claim == null)
                    claim = identity.Claims.FirstOrDefault(x => x.Type.Equals(GeneralConstants.USER_NAME_CLAIM));

                response = (claim != null) ? claim.Value : string.Empty;
            }

            return response;
        }
    }
}
