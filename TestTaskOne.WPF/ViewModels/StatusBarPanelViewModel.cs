using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Options;
using TestTaskOne.DAL;

namespace TestTaskOne.WPF.ViewModels;

internal partial class StatusBarPanelViewModel : ObservableRecipient
{
	private readonly SqlServerDatabaseOptions _databaseOptions;

	public string ConnectionMessage { get; private set; } = null!;

	public StatusBarPanelViewModel(IOptions<SqlServerDatabaseOptions> options)
	{
		_databaseOptions = options.Value;
	}

	protected override void OnActivated()
	{
		base.OnActivated();

		if (!App.ConfiguredDatabaseOptions)
		{
			ConnectionMessage = "Параметры для подключения не были определены.";
			return;
		}

		if (App.ConnectedToDatabase)
		{
			ConnectionMessage = $"Подлюченная БД: {_databaseOptions.DatabaseName}";
		}
		else
		{
			ConnectionMessage = $"Не удалось подключиться к БД: {_databaseOptions.DatabaseName}";
		}
	}
}
