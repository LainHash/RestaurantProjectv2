using DotNetEnv;
using Restaurant.API;


var builder = WebApplication.CreateBuilder(args);

Env.Load();

var searchDir = Directory.GetCurrentDirectory();
while (searchDir is not null)
{
    var envPath = Path.Combine(searchDir, ".env");
    if (File.Exists(envPath)) { Env.Load(envPath); break; }
    searchDir = Directory.GetParent(searchDir)?.FullName;
}

builder.Configuration.AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddServices(builder.Configuration);

var app = builder.Build();

app.UseServices();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
