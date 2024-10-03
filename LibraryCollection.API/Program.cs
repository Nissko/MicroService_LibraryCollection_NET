using EventBus.Abstractions;
using EventBus;
using LibraryCollection.Application.Application.Extensions;
using LibraryCollection.Infrastructure.Extensions;
using EventBusRabbitMQ.AutofacModule;
using Autofac;
using LibraryCollection.API.Infrastructure;
using Autofac.Extensions.DependencyInjection;

/*#region UnitDB
    using (BooksContext db = new BooksContext())
    {
        Book book1 = new Book("Снова живой", "Черные зомби распространяют ядовитый туман по всему городу и за его пределы. Корпораты создают всё больше новых мутантов и бойцов. Демоны затевают что-то недоброе. И все. Все они хотят меня убить…",
            260, 18, new DateTime(2024-01-17).ToUniversalTime(), 119, null);

        book1.AddCategories("Новинки", book1.Id);
        book1.AddGenres("Боевая фантазия", book1.Id);
        book1.AddQuotes("Какая-то цитата", book1.Id);

        db.Books.Add(book1);
        db.SaveChanges();
    }
#endregion*/

ContainerBuilder build = new ContainerBuilder();
build.RegisterModule(new ApplicationModule());

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddLibraryCollectionInfrastructure(builder.Configuration)
    .AddApplication()
    .AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>()
    .AddEventBusRabbitMQConnection(builder.Configuration)
    .AddEventBusRabbitMQConsumer(builder.Configuration.GetValue<string>("EventBusRabbitMQ:EventBusBrokerName"),
                                builder.Configuration.GetValue<string>("EventBusRabbitMQ:EventBusQueueName"));

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