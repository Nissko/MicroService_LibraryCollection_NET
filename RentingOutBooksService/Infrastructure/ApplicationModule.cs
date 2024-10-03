using Autofac;
using EventBus.Abstractions;
using System.Reflection;

namespace RentingOutBooksService.API.Infrastructure
{
    public class ApplicationModule : Autofac.Module
    {
        public ApplicationModule()
        {
        }

        protected override void Load(ContainerBuilder builder)
        {

        }
    }
}
