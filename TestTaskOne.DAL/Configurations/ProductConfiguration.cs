using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTaskOne.Core.Models;

namespace TestTaskOne.DAL.Configurations;

internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
	public void Configure(EntityTypeBuilder<Product> builder)
	{
		builder.HasKey(x => x.Id);

		builder.Property(e => e.Title).IsRequired();

		builder
			.HasMany(e => e.ElementsUsed)
			.WithOne(e => e.Product)
			.HasForeignKey(e => e.ProductId);
	}
}
