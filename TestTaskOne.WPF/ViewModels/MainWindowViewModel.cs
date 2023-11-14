using CommunityToolkit.Mvvm.ComponentModel;

namespace TestTaskOne.WPF.ViewModels;

internal partial class MainWindowViewModel : ObservableObject
{
	public StatusBarPanelViewModel StatusBarPanelViewModel { get; }

	public MenuPanelViewModel MenuPanelViewModel { get; }

	public WorkPanelViewModel WorkPanelViewModel { get; }

	public MainWindowViewModel(
		StatusBarPanelViewModel statusBarPanelViewModel,
		MenuPanelViewModel menuPanelViewModel,
		WorkPanelViewModel workPanelViewModel)
	{
		StatusBarPanelViewModel = statusBarPanelViewModel;
		MenuPanelViewModel = menuPanelViewModel;

		StatusBarPanelViewModel.IsActive = true;
		WorkPanelViewModel = workPanelViewModel;
	}
}
