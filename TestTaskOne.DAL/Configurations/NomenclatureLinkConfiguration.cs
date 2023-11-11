using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTaskOne.Core.Models;

namespace TestTaskOne.DAL.Configurations;

internal class NomenclatureLinkConfiguration : IEntityTypeConfiguration<NomenclatureLink>
{
	public void Configure(EntityTypeBuilder<NomenclatureLink> builder)
	{
		builder.HasKey(e => new { e.ProductId, e.NomenclatureId });
	}
}
