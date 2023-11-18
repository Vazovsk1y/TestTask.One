using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTaskOne.Core.Models;

namespace TestTaskOne.DAL.Configurations;

internal class MaterialConfiguration : IEntityTypeConfiguration<Material>
{
	public void Configure(EntityTypeBuilder<Material> builder)
	{
		builder.UseTptMappingStrategy();
	}
}