using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TestTaskOne.WPF.Infrastructure;
using TestTaskOne.WPF.Windows;

namespace TestTaskOne.WPF.ViewModels;

internal partial class MenuPanelViewModel : ObservableObject
{
	private readonly IUserDialog<ChangeDatabaseWindow> _userDialog;

	public MenuPanelViewModel(IUserDialog<ChangeDatabaseWindow> userDialog)
	{
		_userDialog = userDialog;
	}

	[RelayCommand]
	private void ChangeDatabase()
	{
		_userDialog.ShowDialog();
	}
}
