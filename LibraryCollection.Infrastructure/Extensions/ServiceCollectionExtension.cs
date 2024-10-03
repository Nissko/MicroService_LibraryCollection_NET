using LibraryCollection.Application.Application.Extensions;
using LibraryCollection.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryCollection.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddLibraryCollectionInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddDbContext<BooksContext>(options => options.UseNpgsql(configuration.GetConnectionString("PostgreSqlDatabase")));

            services.AddScoped<IBooksContext>(provider => provider.GetService<BooksContext>());

            services.AddApplication();


            return services;
        }
    }
}
