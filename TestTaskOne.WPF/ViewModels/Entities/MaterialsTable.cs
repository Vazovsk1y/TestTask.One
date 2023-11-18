using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Linq;
using TestTaskOne.DAL;

namespace TestTaskOne.WPF.ViewModels.Entities;

internal partial class MaterialsTable : ObservableRecipient, ITableViewModel
{
	private readonly IServiceScopeFactory _serviceScopeFactory;
	private readonly ObservableCollection<MaterialViewModel> _materials = new();

	public string Title => nameof(TestTaskContext.Materials);

	public ObservableCollection<object> Items => new(_materials);

	public MaterialsTable(IServiceScopeFactory serviceScopeFactory)
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
			var viewModels = dbContext.Materials
			.Select(e => new MaterialViewModel { Title = e.Title })
			.ToList();

			foreach (var item in viewModels)
			{
				_materials.Add(item);
			}
		}

	}
}

internal partial class MaterialViewModel : ObservableObject
{
	[ObservableProperty]
	private string _title = null!;
}