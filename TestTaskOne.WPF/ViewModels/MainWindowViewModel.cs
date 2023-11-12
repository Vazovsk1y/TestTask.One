using CommunityToolkit.Mvvm.ComponentModel;

namespace TestTaskOne.WPF.ViewModels;

internal partial class MainWindowViewModel : ObservableObject
{
	public StatusBarPanelViewModel StatusBarPanelViewModel { get; }

	public MenuPanelViewModel MenuPanelViewModel { get; }

	public MainWindowViewModel(
		StatusBarPanelViewModel statusBarPanelViewModel, 
		MenuPanelViewModel menuPanelViewModel)
	{
		StatusBarPanelViewModel = statusBarPanelViewModel;
		MenuPanelViewModel = menuPanelViewModel;

		StatusBarPanelViewModel.IsActive = true;
	}
}
