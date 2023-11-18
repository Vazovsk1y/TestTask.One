using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using TestTaskOne.Core.Models;
using TestTaskOne.DAL;

namespace TestTaskOne.WPF.ViewModels.Entities;

internal partial class WaybillsTable : ObservableRecipient, ITableViewModel
{
	private readonly IServiceScopeFactory _serviceScopeFactory;
	private readonly ObservableCollection<WaybillViewModel> _waybills = new();
	
	public string Title => nameof(TestTaskContext.Waybills);

	public ObservableCollection<object> Items => new(_waybills);

	public WaybillsTable(IServiceScopeFactory serviceScopeFactory)
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
			var viewModels = dbContext.Waybills
			.Select(e => new WaybillViewModel 
			{
				WaybillId = e.Id, 
				PurchaseCost = e.PurchaseCost, 
				PurchaseDate = e.PurchaseDate, 
				PurchaseItems = string.Join(Environment.NewLine, e.PurchaseItems.Select(e => e.Nomenclature.Title)) })
			.ToList();

			foreach (var item in viewModels)
			{
				_waybills.Add(item);
			}
		}

	}
}

internal partial class WaybillViewModel : ObservableObject
{
	public required Guid WaybillId { get; init; }

	public double PurchaseCost { get; init; }

	public DateTime PurchaseDate { get; init; }

	public string? PurchaseItems { get; init; }
}