using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using TestTaskOne.DAL;

namespace TestTaskOne.WPF.ViewModels.Entities;

internal partial class ProductsTable : ObservableRecipient, ITableViewModel
{
	private readonly IServiceScopeFactory _serviceScopeFactory;
	private readonly ObservableCollection<ProductViewModel> _products = new();

	public string Title => nameof(TestTaskContext.Products);

	public ObservableCollection<object> Items => new(_products);

	public ProductsTable(IServiceScopeFactory serviceScopeFactory)
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
			var viewModels = dbContext.Products
			.Select(e => new ProductViewModel 
			{
				Title = e.Title,
				ElementsUsed = string.Join(Environment.NewLine, e.ElementsUsed.Select(e => e.Nomenclature.Title))
			})
			.ToList();

			foreach (var item in viewModels)
			{
				_products.Add(item);
			}
		}

	}
}

internal partial class ProductViewModel : ObservableObject
{
	public required string Title { get; init; }

	public string? ElementsUsed { get; init; }
}
