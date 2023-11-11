using Microsoft.Extensions.Hosting;
using System.IO;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TestTaskOne.DAL;
using System.Text.Json;

namespace TestTaskOne.WPF;

internal static class Extensions
{
	public static IHostBuilder CreateAssociatedFolder(this IHostBuilder hostBuilder)
	{
		var companyDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), App.CompanyName);
		if (!Directory.Exists(companyDirectory))
		{
			Directory.CreateDirectory(companyDirectory);
		}

		if (!Directory.Exists(App.AssociatedFolderInAppDataPath))
		{
			Directory.CreateDirectory(App.AssociatedFolderInAppDataPath);
		}

		return hostBuilder;
	}

	public static IServiceCollection AddWPF(this IServiceCollection services) => services
		.AddSingleton<MainWindow>()
		.AddSingleton<IOptions<SqlServerDatabaseOptions>, SqlServerDatabaseOptions>(e =>
		{
			string databaseConfigFile = Path.Combine(App.AssociatedFolderInAppDataPath, DatabaseConfig.FileName);
			if (!File.Exists(databaseConfigFile))
			{
				return SqlServerDatabaseOptions.Default;
			}

			using var stream = File.OpenRead(databaseConfigFile);
			var config = JsonSerializer.Deserialize<DatabaseConfig>(stream) ?? throw new InvalidOperationException("Deserialized object was equal to null.");
			return new SqlServerDatabaseOptions
			{
				DatabaseName = config.DatabaseName,
				Password = config.Password,
				UserName = config.UserName,
			};
		});
}

internal class DatabaseConfig
{
	public const string FileName = "databaseConfig.json";
	public string? UserName { get; set; }

	public string? Password { get; set; }

	public required string DatabaseName { get; set; }
}
