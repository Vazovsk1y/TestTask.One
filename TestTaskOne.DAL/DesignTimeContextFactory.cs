using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Diagnostics;

namespace TestTaskOne.DAL;

public class DesignTimeContextFactory : IDesignTimeDbContextFactory<TestTaskContext>
{
	public TestTaskContext CreateDbContext(string[] args)
	{
		var optionsBuilder = new DbContextOptionsBuilder();
		var designTimeOptions = new SqlServerDatabaseOptions("TestTaskDb");
		optionsBuilder.UseSqlServer(designTimeOptions.BuildConnectionString());
		return new TestTaskContext(optionsBuilder.Options);
	}
}
