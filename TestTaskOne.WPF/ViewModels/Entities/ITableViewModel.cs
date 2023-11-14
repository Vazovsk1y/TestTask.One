using System.Collections.ObjectModel;

namespace TestTaskOne.WPF.ViewModels.Entities;

public interface ITableViewModel
{
	string Title { get; }

	ObservableCollection<object> Items { get; }
}
