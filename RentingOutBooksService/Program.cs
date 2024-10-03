using EventBus.Abstractions;
using EventBus;
using RentingOutBooksService.Application.Application.Extensions;
using RentingOutBooksService.Infrastructure.Extensions;
using EventBusRabbitMQ.AutofacModule;
using RentingOutBooksService.API.Infrastructure;
using Autofac;
using Autofac.Extensions.DependencyInjection;

ContainerBuilder build = new ContainerBuilder();
build.RegisterModule(new ApplicationModule());

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddRentingOutBooksInfrastructure(builder.Configuration)
    .AddApplication()
    .AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>()
    .AddEventBusRabbitMQConnection(builder.Configuration)
    .AddEventBusRabbitMQConsumer(builder.Configuration.GetValue<string>("EventBusRabbitMQ:EventBusBrokerName"),
                                builder.Configuration.GetValue<string>("EventBusRabbitMQ:EventBusQueueName"))
    .AddEventBusRabbitMQPublisher(builder.Configuration.GetValue<string>("EventBusRabbitMQ:EventBusBrokerName"));

builder.Host.UseServiceProviderFactory(new AutofacChildLifetimeScopeServiceProviderFactory(build.Build()));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.SubscribeToEvents();

app.Run();
