using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using TestTaskOne.WPF.ViewModels.Entities;

namespace TestTaskOne.WPF.ViewModels;

internal partial class WorkPanelViewModel : ObservableObject
{
	public ObservableCollection<ITableViewModel> Tables { get; }

	[ObservableProperty]
	private ITableViewModel? _selectedTable;
	public WorkPanelViewModel(IServiceProvider serviceProvider)
	{
		Tables = new ObservableCollection<ITableViewModel>(serviceProvider.GetServices<ITableViewModel>());

        foreach (var item in Tables)
        {
			if (item is ObservableRecipient recipient)
			{
				recipient.IsActive = true;
			}
        }
    }
}
