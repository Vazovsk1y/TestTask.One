using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTaskOne.Core.Models;

namespace TestTaskOne.DAL.Configurations;

internal class WaybillConfiguration : IEntityTypeConfiguration<Waybill>
{
	public void Configure(EntityTypeBuilder<Waybill> builder)
	{
		builder.HasKey(e => e.Id);

		builder.Property(e => e.PurchaseCost).IsRequired();

		builder.Property(e => e.PurchaseDate).IsRequired();

		builder.HasMany(e => e.PurchaseItems)
			.WithOne(e => e.Waybill)
			.HasForeignKey(e => e.WaybillId);
	}
}