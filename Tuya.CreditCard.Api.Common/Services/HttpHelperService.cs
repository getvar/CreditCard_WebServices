using Microsoft.AspNetCore.Http;
using Tuya.CreditCard.Api.Common.Contracts;

namespace Tuya.CreditCard.Api.Common.Services
{
    public class HttpHelperService : IHttpHelperService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpHelperService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetBasePath()
        {
            var request = _httpContextAccessor.HttpContext?.Request;

            if (request == null)
                throw new InvalidOperationException("Las solicitudes HTTP no están activas.");

            return $"{request.Scheme}://{request.Host}{request.PathBase}";
        }
    }
}
