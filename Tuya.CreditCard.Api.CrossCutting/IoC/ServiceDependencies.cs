using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuya.CreditCard.Api.CrossCutting.IoC
{
    public static class ServiceDependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            //services.AddTransient<ICriteriaSettingService, CriteriaSettingService>();
            return services;
        }
    }
}
