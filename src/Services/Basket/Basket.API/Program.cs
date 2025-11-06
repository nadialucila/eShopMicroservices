var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;
var database = builder.Configuration.GetConnectionString("Database")!;

//services
builder.Services.AddCarter();
builder.Services.AddMarten(opts =>
{
    opts.Connection(database);
}).UseLightweightSessions();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

//http pipeline
app.MapCarter();
app.UseExceptionHandler(options => { });

app.Run();
