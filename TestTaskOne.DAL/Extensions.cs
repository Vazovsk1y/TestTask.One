using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace TestTaskOne.DAL;

public static class Extensions
{
	public static void Seed(this ModelBuilder modelBuilder)
	{
		// TODO implement data seeding
	}
	public static IServiceCollection AddDAL(this IServiceCollection services)
	{
		services
			.AddDbContext<TestTaskContext>((provider, e) =>
			{
				var databaseOptions = provider.GetRequiredService<IOptions<SqlServerDatabaseOptions>>();
				string connectionString = databaseOptions.Value.BuildConnectionString();
				e.UseSqlServer(connectionString);
			})
		;

		return services;
	}
}
