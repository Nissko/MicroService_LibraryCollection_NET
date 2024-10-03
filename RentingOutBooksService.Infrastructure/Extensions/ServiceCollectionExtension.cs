using RentingOutBooksService.Application.Application.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RentingOutBooksService.Application.Common.Interfaces;

namespace RentingOutBooksService.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddRentingOutBooksInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddDbContext<RentMicroServiceContext>(options => options.UseNpgsql(configuration.GetConnectionString("PostgreSqlDatabase")));

            services.AddScoped<IRentMicroServiceContext>(provider => provider.GetService<RentMicroServiceContext>());

            services.AddApplication();

            return services;
        }
    }
}
