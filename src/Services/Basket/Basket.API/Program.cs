var builder = WebApplication.CreateBuilder(args);
//services
var app = builder.Build();
//http pipeline
app.Run();
