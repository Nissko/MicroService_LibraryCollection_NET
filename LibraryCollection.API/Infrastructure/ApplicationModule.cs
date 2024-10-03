using Autofac;
using EventBus.Abstractions;
using LibraryCollection.Application.Application.IntegrationEvents.EventHandler;
using System.Reflection;

namespace LibraryCollection.API.Infrastructure
{
    public class ApplicationModule : Autofac.Module
    {
        public ApplicationModule()
        {
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(ChangeBookEventHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));
            builder.RegisterAssemblyTypes(typeof(RefundBookEventHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));
        }
    }
}
