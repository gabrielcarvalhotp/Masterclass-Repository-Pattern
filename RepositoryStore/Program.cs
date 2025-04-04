using Microsoft.EntityFrameworkCore;
using RepositoryStore.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Connection");
var serverVersion = new MySqlServerVersion(new Version(8, 0, 35));

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseMySql(connectionString, serverVersion);
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();


