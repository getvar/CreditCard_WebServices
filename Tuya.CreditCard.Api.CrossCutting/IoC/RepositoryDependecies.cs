using Microsoft.Extensions.DependencyInjection;
using Tuya.CreditCard.Api.DAL.Contracts.Repositories;
using Tuya.CreditCard.Api.DAL.Repositories;

namespace Tuya.CreditCard.Api.CrossCutting.IoC
{
    public static class RepositoryDependecies
    {
        public static IServiceCollection AddRepositoryDependencies(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICardRepository, CardRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ISaleRepository, SaleRepository>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();
            return services;
        }
    }
}
