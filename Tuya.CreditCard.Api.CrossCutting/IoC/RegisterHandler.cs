using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tuya.CreditCard.Api.DAL;
using Tuya.CreditCard.Api.DAL.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Tuya.CreditCard.Api.CrossCutting.IoC
{
    public static class RegisterHandler
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICreditCardContext, CreditCardContext>();
            services.AddDbContext<CreditCardContext>((provider, options) =>
            {
                var connectionString = configuration.GetConnectionString("ApiConnection");
                options.UseSqlServer(connectionString);
            });

            services.AddServiceDependencies();
            services.AddRepositoryDependencies();
        }
    }
}
