using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTaskOne.Core.Models;

namespace TestTaskOne.DAL.Configurations;

internal class ComponentConfiguration : IEntityTypeConfiguration<Component>
{
	public void Configure(EntityTypeBuilder<Component> builder)
	{
		builder.UseTptMappingStrategy();
	}
}