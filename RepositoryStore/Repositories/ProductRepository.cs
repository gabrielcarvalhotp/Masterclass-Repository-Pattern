using Microsoft.EntityFrameworkCore;
using RepositoryStore.Data;
using RepositoryStore.Models;
using RepositoryStore.Repositories.Abstractions;

namespace RepositoryStore.Repositories;
public class ProductRepository(AppDbContext context) : IProductRepository
{
    public async Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default)
    {
        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();
        return product;
    }

    public async Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken = default)
    {
        context.Products.Update(product);
        await context.SaveChangesAsync();
        return product;
    }

    public async Task DeleteAsync(Product product, CancellationToken cancellationToken = default)
    {
        product.Active = false;
        context.Update(product);       
        await context.SaveChangesAsync();
    }

    public async Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context
            .Products
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
    public async Task<List<Product>> GetAllAsync(int skip = 0, int take = 25, CancellationToken cancellationToken = default)
    {
        return await context
            .Products
            .AsNoTracking()
            .Skip(skip)
            .Take(take)
            .Where(x => x.Active)
            .ToListAsync(cancellationToken);
    }
}
