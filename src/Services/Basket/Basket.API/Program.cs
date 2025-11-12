var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;
var database = builder.Configuration.GetConnectionString("Database")!;

ConfigureServices(builder);

var app = builder.Build();

ConfigurePipeline(app);

app.Run();

#region Services
void ConfigureServices(WebApplicationBuilder builder){
    builder.Services.AddCarter();

    builder.Services.AddMarten(opts =>
    {
        opts.Connection(database);
        //another way to configure id
        //opts.Schema.For<ShoppingCart>().Identity(x => x.Username);
    }).UseLightweightSessions();

    //repository                 
    builder.Services.AddScoped<IBasketRepository, BasketRepository>();
    //one way to register decorator
    //builder.Services.AddScoped<IBasketRepository>(provider =>
    //{
    //    var basketRepository = provider.GetRequiredService<BasketRepository>();
    //    return new CachedBasketRepository(basketRepository, provider.GetRequiredService<IDistributedCache>());
    //});
    builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();

    builder.Services.AddStackExchangeRedisCache(options =>
    {
        options.Configuration = builder.Configuration.GetConnectionString("Redis");
        //options.IntanceName = "Basket";
    });

    builder.Services.AddMediatR(config =>
    {
        config.RegisterServicesFromAssembly(assembly);
        config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        config.AddOpenBehavior(typeof(LoggingBehavior<,>));
    });

    builder.Services.AddValidatorsFromAssembly(assembly);

    builder.Services.AddExceptionHandler<CustomExceptionHandler>();
}
#endregion

#region Http Pipeline
void ConfigurePipeline(WebApplication app)
{
    app.MapCarter();
    app.UseExceptionHandler(options => { });
}
#endregion