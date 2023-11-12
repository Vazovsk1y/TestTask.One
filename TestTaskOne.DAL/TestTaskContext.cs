using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TestTaskOne.Core.Models;

namespace TestTaskOne.DAL;

public class TestTaskContext : DbContext
{
	public DbSet<Component> Components { get; set; }

	public DbSet<Material> Materials { get; set; }

	public DbSet<NomenclatureLink> NomenclaturesLinks { get; set; }

	public DbSet<Product> Products { get; set; }

	public DbSet<Waybill> Waybills { get; set; }

	public DbSet<WaybillItem> WaybillItems { get; set; }

	public DbSet<Nomenclature> Nomenclatures { get; set; }

	public TestTaskContext(DbContextOptions options) : base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		base.OnModelCreating(modelBuilder);

		modelBuilder.Seed();
	}
	public static bool CanConnect(string connectionString)
	{
		var optionsBuilder = new DbContextOptionsBuilder<TestTaskContext>()
			.UseSqlServer(connectionString);

		using var dbContext = new TestTaskContext(optionsBuilder.Options);
		return dbContext.Database.CanConnect();
	}
}
