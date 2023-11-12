using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using TestTaskOne.DAL;
using TestTaskOne.WPF.Windows;

namespace TestTaskOne.WPF;

public partial class App : Application
{
	#region --Fields--

	private static IHost? _host;

	public const string Name = "TestTaskOne";

	public const string CompanyName = "Vazovskiy";

	#endregion

	#region --Properties--

	public static string WorkingDirectory => IsDesignMode ? Path.GetDirectoryName(GetSourceCodePath())! : Environment.CurrentDirectory;

	public static string AssociatedFolderInAppDataPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), CompanyName, Name);

	public static bool IsDesignMode { get; private set; } = true;

	public static bool ConnectedToDatabase { get; private set; } = false;

	public static bool ConfiguredDatabaseOptions { get; private set; } = false;

	public static IServiceProvider Services => Host.Services;

	public static IHost Host => _host ??= Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

	#endregion

	#region --Constructors--

	public App()
	{
	}

	#endregion

	#region --Methods--

	public static void ConfigureServices(HostBuilderContext host, IServiceCollection services) => services
		.AddDAL()
		.AddWPF()
		;

	private static string GetSourceCodePath([CallerFilePath] string? path = null) => string.IsNullOrWhiteSpace(path)
		? throw new ArgumentNullException(nameof(path)) : path;

	public void StartGlobalExceptionsHandling()
	{
		DispatcherUnhandledException += (sender, e) =>
		{
			var logger = Services.GetRequiredService<ILogger<App>>();
			logger.LogError(e.Exception, "Something went wrong in [{nameofDispatcherUnhandledException}]", nameof(DispatcherUnhandledException));
			e.Handled = true;
			Current?.Shutdown();
		};

		AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
		{
			var logger = Services.GetRequiredService<ILogger<App>>();
			logger.LogError(e.ExceptionObject as Exception, "Something went wrong in [{nameofCurrentDomainUnhandledException}].", nameof(AppDomain.CurrentDomain.UnhandledException));
		};

		TaskScheduler.UnobservedTaskException += (sender, e) =>
		{
			var logger = Services.GetRequiredService<ILogger<App>>();
			logger.LogError(e.Exception, "Something went wrong in [{nameofCurrentDomainUnhandledException}].", nameof(TaskScheduler.UnobservedTaskException));
		};
	}

	protected override void OnStartup(StartupEventArgs e)
	{
		IsDesignMode = false;
		base.OnStartup(e);
		Host.Start();

		var options = Services.GetRequiredService<IOptions<SqlServerDatabaseOptions>>();
		ConfiguredDatabaseOptions = options.Value != SqlServerDatabaseOptions.Undefined;

		if (ConfiguredDatabaseOptions)
		{
			ConnectedToDatabase = TestTaskContext.CanConnect(options.Value.BuildConnectionString()!);
		}

		Services.GetRequiredService<MainWindow>().Show();
	}

	#endregion
}
