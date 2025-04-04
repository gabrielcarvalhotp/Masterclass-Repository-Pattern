using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RepositoryStore.Models;

namespace RepositoryStore.Data.Mapping;

public class ProductMap: IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");

        builder.HasKey(x =>x.Id);

        builder.Property (x=> x.Description)
            .IsRequired (true)
            .HasMaxLength(160)
            .HasColumnType("varchar");
    }
}