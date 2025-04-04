using Microsoft.EntityFrameworkCore;
using RepositoryStore.Data;
using RepositoryStore.Migrations;
using RepositoryStore.Models;
using RepositoryStore.Repositories;
using RepositoryStore.Repositories.Abstractions;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Connection");
var serverVersion = new MySqlServerVersion(new Version(8, 0, 35));

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseMySql(connectionString, serverVersion);
});

builder.Services.AddTransient<IProductRepository, ProductRepository>();

var app = builder.Build();

app.MapGet("/v1/products", async (CancellationToken cancellationToken, IProductRepository repository, int skip = 0, int take = 25)
    => Results.Ok(await repository.GetAllAsync(skip, take, cancellationToken)));

app.MapGet("/v1/products/{id}", async (CancellationToken cancellationToken, IProductRepository repository, int id)
    => Results.Ok(await repository.GetByIdAsync(id, cancellationToken)));

app.MapPost("/v1/products", async (CancellationToken cancellationToken, IProductRepository repository, Product product)
    => Results.Created(string.Empty, await repository.CreateAsync(product, cancellationToken)));

app.MapPut("/v1/products", async (CancellationToken cancellationToken, IProductRepository repository, Product product)
    => Results.Ok(await repository.UpdateAsync(product, cancellationToken)));

app.MapDelete("/v1/products/{id}", async (CancellationToken cancellationToken, IProductRepository repository, int id) =>
{
    var product = await repository.GetByIdAsync(id, cancellationToken);
    if (product == null)
    {
        Results.NotFound();
        return;
    }

    await repository.DeleteAsync(product!);
    Results.NoContent();
});

app.Run();


