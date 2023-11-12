using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using TestTaskOne.DAL;
using TestTaskOne.WPF.Infrastructure;
using TestTaskOne.WPF.Windows;

namespace TestTaskOne.WPF.ViewModels;

internal partial class ChangeDatabaseViewModel : ObservableObject
{
	private readonly IUserDialog<ChangeDatabaseWindow> _userDialog;

	[ObservableProperty]
	[NotifyCanExecuteChangedFor(nameof(AcceptCommand))]
	private string _databaseName = null!;

	[ObservableProperty]
	[NotifyCanExecuteChangedFor(nameof(AcceptCommand))]
	private string? _userName;

	[ObservableProperty]
	[NotifyCanExecuteChangedFor(nameof(AcceptCommand))]
	private string? _password;

	public ChangeDatabaseViewModel(IUserDialog<ChangeDatabaseWindow> userDialog)
	{
		_userDialog = userDialog;
	}

	[RelayCommand(CanExecute = nameof(OnCanAccept))]
	private void Accept()
	{
		var options = new SqlServerDatabaseOptions (DatabaseName, Password, UserName);
		var connectionString = options.BuildConnectionString();
		bool canConnect = TestTaskContext.CanConnect(connectionString);
		if (canConnect)
		{
			MessageBox.Show($"Connection to {DatabaseName} failed.", "Fail", MessageBoxButton.OK, MessageBoxImage.Error);
			_userDialog.CloseDialog();
			return;
		}

		using var writer = new StreamWriter(DatabaseConfig.Path);
		var config = new DatabaseConfig { DatabaseName = options.DatabaseName, Password = options.Password, UserName = options.UserName };
		string json = JsonSerializer.Serialize(config);
		writer.Write(json);
		MessageBox.Show($"Restart the application to reconnect to new database.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
		_userDialog.CloseDialog();
	}

	private bool OnCanAccept()
	{
		return !string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password) && string.IsNullOrWhiteSpace(UserName);
	}

	[RelayCommand]
	private void Cancel() => _userDialog.CloseDialog();
}
