using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Tuya.CreditCard.Api.App.Contracts.Services;
using Tuya.CreditCard.Api.App.Services;
using Tuya.CreditCard.Api.Common.Contracts;
using Tuya.CreditCard.Api.Common.Services;
using Tuya.CreditCard.Api.DAL.Mappers.Profiles;

namespace Tuya.CreditCard.Api.CrossCutting.IoC
{
    public static class ServiceDependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IApiAccessorUserData, ApiAccessorUserData>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddAutoMapper(typeof(MappingProfile));
            return services;
        }
    }
}
