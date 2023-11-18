﻿using Microsoft.Extensions.Hosting;
using System.Threading;
using System;
using Serilog;
using System.IO;

namespace TestTaskOne.WPF;

internal class Program
{
	public static bool IsInDebug { get; private set; }

	private static Mutex? _mutex;

	[STAThread]
	public static void Main(string[] args)
	{
		_mutex = new Mutex(true, App.Name, out bool createdNew);
		if (!createdNew)
		{
			return;
		}

#if DEBUG
		IsInDebug = true;
#endif

		App app = new();
		app.Run();
	}

	public static IHostBuilder CreateHostBuilder(string[] args)
	{
		Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", IsInDebug ? "Development" : "Production");

		return Host
		.CreateDefaultBuilder(args)
		.CreateAssociatedFolder()
		.ConfigureAppConfiguration((a, e) =>
		{
			a.HostingEnvironment.ContentRootPath = App.WorkingDirectory;
			a.HostingEnvironment.ApplicationName = App.Name;
		})
		.UseSerilog((host, loggingConfiguration) =>
		{
			string logFileName = "log.txt";
			string logDirectory = Path.Combine(App.AssociatedFolderInAppDataPath, "logs");
			if (!Directory.Exists(logDirectory))
			{
				Directory.CreateDirectory(logDirectory);
			}

			string logFileFullPath = Path.Combine(logDirectory, logFileName);
			loggingConfiguration.MinimumLevel.Information();

			if (host.HostingEnvironment.IsDevelopment())
			{
				loggingConfiguration.WriteTo.Debug();
			}
			else
			{
				loggingConfiguration.WriteTo.File(logFileFullPath, rollingInterval: RollingInterval.Day);
			}
		})
		.ConfigureServices(App.ConfigureServices)
		;
	}
}
