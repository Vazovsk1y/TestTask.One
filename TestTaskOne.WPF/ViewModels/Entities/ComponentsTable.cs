using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Linq;
using TestTaskOne.DAL;

namespace TestTaskOne.WPF.ViewModels.Entities;

internal partial class ComponentsTable : ObservableRecipient, ITableViewModel
{ 
	private readonly IServiceScopeFactory _serviceScopeFactory;
	private readonly ObservableCollection<ComponentViewModel> _components = new();

	public string Title => nameof(TestTaskContext.Components);

	public ObservableCollection<object> Items => new(_components);

	public ComponentsTable(IServiceScopeFactory serviceScopeFactory)
	{
		_serviceScopeFactory = serviceScopeFactory;
	}

	protected override void OnActivated()
	{
		base.OnActivated();

		using var scope = _serviceScopeFactory.CreateScope();
		var dbContext = scope.ServiceProvider.GetRequiredService<TestTaskContext>();

		if (App.ConnectedToDatabase)
		{
			var viewModels = dbContext.Components
			.Select(e => new ComponentViewModel { Title = e.Title })
			.ToList();

            foreach (var item in viewModels)
            {
				_components.Add(item);
            }
        }

	}
}

internal partial class ComponentViewModel : ObservableObject
{
	public required string Title { get; init; }
}