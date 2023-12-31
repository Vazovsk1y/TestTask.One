﻿using Microsoft.Extensions.Hosting;
using System.IO;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TestTaskOne.DAL;
using System.Text.Json;
using TestTaskOne.WPF.ViewModels;
using TestTaskOne.WPF.Windows;
using TestTaskOne.WPF.Infrastructure;
using TestTaskOne.WPF.ViewModels.Entities;

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
		.AddSingleton(e =>
		{
			var viewModel = e.GetRequiredService<MainWindowViewModel>();
			return new MainWindow { DataContext = viewModel };
		})
		.AddTransient(e =>
		{
			var viewModel = e.GetRequiredService<ChangeDatabaseViewModel>();
			return new ChangeDatabaseWindow { DataContext = viewModel };
		})
		.AddSingleton<MainWindowViewModel>()
		.AddSingleton<StatusBarPanelViewModel>()
		.AddSingleton<MenuPanelViewModel>()
		.AddSingleton<WorkPanelViewModel>()
		.AddScoped<ChangeDatabaseViewModel>()
		.AddSingleton(typeof(IUserDialog<>), typeof(BaseUserDialogService<>))
		.AddSingleton<IOptions<SqlServerDatabaseOptions>, SqlServerDatabaseOptions>(e =>
		{
			if (!File.Exists(DatabaseConfig.Path))
			{
				return SqlServerDatabaseOptions.Undefined;
			}

			using var stream = File.OpenRead(DatabaseConfig.Path);
			var config = JsonSerializer.Deserialize<DatabaseConfig>(stream) ?? throw new InvalidOperationException("Deserialized object was equal to null.");

			return new SqlServerDatabaseOptions(config.DatabaseName, config.Password, config.UserName);
		})
		.AddSingleton<ITableViewModel, ComponentsTable>()
		.AddSingleton<ITableViewModel, MaterialsTable>()
		.AddSingleton<ITableViewModel, ProductsTable>()
		.AddSingleton<ITableViewModel, WaybillsTable>();
}

internal class DatabaseConfig
{
	public static readonly string Path = System.IO.Path.Combine(App.AssociatedFolderInAppDataPath, "databaseConfig.json");
	public string? UserName { get; set; }

	public string? Password { get; set; }

	public required string DatabaseName { get; set; }
}
