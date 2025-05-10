using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuya.CreditCard.Api.CrossCutting.IoC
{
    public static class RepositoryDependecies
    {
        public static IServiceCollection AddRepositoryDependencies(this IServiceCollection services)
        {
            //services.AddTransient<IMaterialListRepository, MaterialListRepository>();
            return services;
        }
    }
}
