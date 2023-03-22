using CardManager.Application;
using CardManager.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Service layer
builder.Services.AddApplication();

// Infrastructure layer
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
