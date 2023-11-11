using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTaskOne.Core.Models;

namespace TestTaskOne.DAL.Configurations;

internal class WaybillItemConfiguration : IEntityTypeConfiguration<WaybillItem>
{
	public void Configure(EntityTypeBuilder<WaybillItem> builder)
	{
		builder.HasKey(e => new { e.WaybillId, e.NomenclatureId });
	}
}
