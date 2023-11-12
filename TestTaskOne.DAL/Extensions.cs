using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TestTaskOne.Core.Models;

namespace TestTaskOne.DAL;

public static class Extensions
{
	public static void Seed(this ModelBuilder modelBuilder)
	{
		#region --Nomenclatures--

		var components = new List<Component>()
		{
			new Component { Title = "Болт" },
			new Component { Title = "КПП" },
			new Component { Title = "Шестеренка" },
			new Component { Title = "Плата управления" },
		};

		var materials = new List<Material>()
		{
			new Material { Title = "Резина" },
			new Material { Title = "Аллюминий" },
			new Material { Title = "Стекло" },
		};

		modelBuilder.Entity<Component>().HasData(components);
		modelBuilder.Entity<Material>().HasData(materials);

		#endregion

		#region --Products--

		var firtsProduct = new Product { Title = "Легковой автомобиль" };
		var secondProduct = new Product { Title = "Персональный компьютер" };
		var thirdProduct = new Product { Title = "Подъемный кран" };

		var nomenclaturesLinks = new List<NomenclatureLink>()
		{
			new NomenclatureLink { NomenclatureId = components[0].Id, ProductId = firtsProduct.Id },
		    new NomenclatureLink { NomenclatureId = components[1].Id, ProductId = firtsProduct.Id },
	        new NomenclatureLink { NomenclatureId = components[2].Id, ProductId = firtsProduct.Id },
	        new NomenclatureLink { NomenclatureId = materials[0].Id, ProductId = firtsProduct.Id },
		    new NomenclatureLink { NomenclatureId = materials[1].Id, ProductId = firtsProduct.Id },
		    new NomenclatureLink { NomenclatureId = components[0].Id, ProductId = secondProduct.Id },
			new NomenclatureLink { NomenclatureId = components[3].Id, ProductId = secondProduct.Id },
			new NomenclatureLink { NomenclatureId = materials[1].Id, ProductId = secondProduct.Id },
		    new NomenclatureLink { NomenclatureId = components[0].Id, ProductId = thirdProduct.Id },
			new NomenclatureLink { NomenclatureId = components[1].Id, ProductId = thirdProduct.Id },
			new NomenclatureLink { NomenclatureId = components[2].Id, ProductId = thirdProduct.Id },
			new NomenclatureLink { NomenclatureId = components[3].Id, ProductId = thirdProduct.Id },
			new NomenclatureLink { NomenclatureId = materials[0].Id, ProductId = thirdProduct.Id },
			new NomenclatureLink { NomenclatureId = materials[1].Id, ProductId = thirdProduct.Id },
			new NomenclatureLink { NomenclatureId = materials[2].Id, ProductId = thirdProduct.Id },
		};

		var products = new List<Product>()
		{
			firtsProduct,
			secondProduct,
			thirdProduct,
		};

		modelBuilder.Entity<Product>().HasData(products);
		modelBuilder.Entity<NomenclatureLink>().HasData(nomenclaturesLinks);

		#endregion

		#region --Waybills--

		const int requiredYearCount = 3;
		var waybills = new List<Waybill>();
		var waybillsItems = new List<WaybillItem>();
		int currentYear = DateTime.Now.Year;
		var random = new Random();

		for (int i = 0; i < requiredYearCount * 2; i++)
		{
			if (i % 2 == 0 && i != 0)
			{
				currentYear -= 1;
			}

			var waybill = new Waybill()
			{
				PurchaseCost = random.Next(1000, 10000),
				PurchaseDate = new DateTime(currentYear, random.Next(1, 12), random.Next(1, 28), random.Next(1, 23), random.Next(1, 60), random.Next(1, 60))
			};
			waybillsItems.Add(new WaybillItem { NomenclatureId = components[0].Id, WaybillId = waybill.Id });
			waybillsItems.Add(new WaybillItem { NomenclatureId = components[1].Id, WaybillId = waybill.Id });
			waybillsItems.Add(new WaybillItem { NomenclatureId = components[2].Id, WaybillId = waybill.Id });
			waybillsItems.Add(new WaybillItem { NomenclatureId = components[3].Id, WaybillId = waybill.Id });
			waybillsItems.Add(new WaybillItem { NomenclatureId = materials[0].Id, WaybillId = waybill.Id });
			waybillsItems.Add(new WaybillItem { NomenclatureId = materials[1].Id, WaybillId = waybill.Id });
			waybillsItems.Add(new WaybillItem { NomenclatureId = materials[2].Id, WaybillId = waybill.Id });

			waybills.Add(waybill);
		}

		modelBuilder.Entity<Waybill>().HasData(waybills);
		modelBuilder.Entity<WaybillItem>().HasData(waybillsItems);

		#endregion
	}

	public static IServiceCollection AddDAL(this IServiceCollection services)
	{
		services
			.AddDbContext<TestTaskContext>((provider, options) =>
			{
				var databaseOptions = provider.GetRequiredService<IOptions<SqlServerDatabaseOptions>>();
				if (databaseOptions.Value != SqlServerDatabaseOptions.Undefined)
				{
					string? connectionString = databaseOptions.Value.BuildConnectionString();
					options.UseSqlServer(connectionString);
				}
			})
		;

		return services;
	}
}
