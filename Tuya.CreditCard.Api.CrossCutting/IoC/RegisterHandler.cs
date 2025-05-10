using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuya.CreditCard.Api.CrossCutting.IoC
{
    public static class RegisterHandler
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddScoped<IItemContext, ItemContext>();
            //services.AddDbContext<ItemContext>((provider, options) =>
            //{
            //    var encryptionService = encryptionServiceFactory(provider);
            //    var encryptedConnectionString = configuration.GetConnectionString("ApiConnection");
            //    var decryptedConnectionString = encryptionService.DecryptData(encryptedConnectionString!);
            //    options.UseSqlServer(decryptedConnectionString);
            //});

            services.AddServiceDependencies();
            services.AddRepositoryDependencies();
        }
    }
}
