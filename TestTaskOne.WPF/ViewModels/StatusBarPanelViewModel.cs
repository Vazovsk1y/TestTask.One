using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Options;
using TestTaskOne.DAL;

namespace TestTaskOne.WPF.ViewModels;

internal partial class StatusBarPanelViewModel : ObservableObject
{
	public SqlServerDatabaseOptions DatabaseOptions { get; }

	public StatusBarPanelViewModel(IOptions<SqlServerDatabaseOptions> options)
	{
		DatabaseOptions = options.Value;
	}
}
