using BuildingBlocks.Behaviors;

var builder = WebApplication.CreateBuilder(args);

//add services to the container
var assembly = typeof(Program).Assembly;
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(assembly);

var app = builder.Build();

//configure the http request pipeline
app.MapCarter();

app.Run();
