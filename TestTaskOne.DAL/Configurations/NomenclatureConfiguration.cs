using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTaskOne.Core.Models;

namespace TestTaskOne.DAL.Configurations;

internal class NomenclatureConfiguration : IEntityTypeConfiguration<Nomenclature>
{
	public void Configure(EntityTypeBuilder<Nomenclature> builder)
	{
		builder.HasKey(x => x.Id);

		builder.UseTptMappingStrategy();

		builder.Property(e => e.Title).IsRequired();
	}
}
