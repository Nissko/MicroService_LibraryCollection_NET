using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace RentingOutBooksService.Application.Application.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
