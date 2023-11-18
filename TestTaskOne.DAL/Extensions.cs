using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TestTaskOne.Core.Models;
using DocumentFormat.OpenXml.Wordprocessing;
using static TestTaskOne.DAL.DocxModels;

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

	public static async Task SaveInDocxAsync(this TestTaskContext context, string docxFileFolder, DocxDocumentOptions docxDocumentOptions, CancellationToken cancellationToken = default)
	{
		string fileName = $"summary - {Guid.NewGuid()}.docx";
		string filePath = Path.Combine(docxFileFolder, fileName);

		using var docxFile = WordprocessingDocument.Create(filePath, WordprocessingDocumentType.Document);
		var mainPart = docxFile.AddMainDocumentPart();
		var document = new Document();
		mainPart.Document = document;

		var body = new Body();
		var componentsTable = CreateTable<ComponentDocxModel>(body, nameof(TestTaskContext.Components), docxDocumentOptions);
		body.Append(componentsTable.tableTitle);
		var componentsTableModels = await context
			.Components
			.Select(e => new ComponentDocxModel { Title = e.Title }).ToListAsync(cancellationToken);

		FillTable(componentsTable.table, componentsTableModels, docxDocumentOptions);
		body.Append(componentsTable.table);

		var materialsTable = CreateTable<MaterialDocxModel>(body, nameof(TestTaskContext.Materials), docxDocumentOptions);
		body.Append(materialsTable.tableTitle);
		var materialsTableModels = await context
			.Materials
			.Select(e => new MaterialDocxModel { Title = e.Title })
			.ToListAsync(cancellationToken);

		FillTable(materialsTable.table, materialsTableModels, docxDocumentOptions);
		body.Append(materialsTable.table);

		var productsTable = CreateTable<ProductDocxModel>(body, nameof(TestTaskContext.Products), docxDocumentOptions);
		body.Append(productsTable.tableTitle);
		var productsTableModels = await context
			.Products
			.Include(e => e.ElementsUsed)
			.ThenInclude(e => e.Nomenclature)
			.Select(e => new ProductDocxModel 
			{ 
				Title = e.Title, 
				ElementsUsed = string.Join($",{Environment.NewLine}", e.ElementsUsed.Select(i => i.Nomenclature.Title))
			})
			.ToListAsync(cancellationToken);

		FillTable(productsTable.table, productsTableModels, docxDocumentOptions);
		body.Append(productsTable.table);

		var waybillsTable = CreateTable<WaybillDocxModel>(body, nameof(TestTaskContext.Waybills), docxDocumentOptions);
		body.Append(waybillsTable.tableTitle);

		var waybillsTableModels = await context
			.Waybills
			.Include(e => e.PurchaseItems)
			.ThenInclude(e => e.Nomenclature)
			.Select(e => new WaybillDocxModel
			{
				Id = e.Id.ToString(),
				PurchaseCost = e.PurchaseCost.ToString(),
				PurchaseDate = e.PurchaseDate.ToString(),
				PurchaseItems = string.Join($",{Environment.NewLine}", e.PurchaseItems.Select(e => e.Nomenclature.Title)),
			})
			.ToListAsync(cancellationToken);

		FillTable(waybillsTable.table, waybillsTableModels, docxDocumentOptions);
		body.Append(waybillsTable.table);

		document.Append(body);
	}

	private static (Table table, Paragraph tableTitle) CreateTable<T>(Body body, string tableTitle, DocxDocumentOptions docxDocumentOptions) where T : class
	{
		var titleParagraph = new Paragraph();
		var titleRun = new Run(new Text(tableTitle))
		{
			RunProperties = new RunProperties
			{
				RunFonts = new RunFonts
				{
					Ascii = docxDocumentOptions.DocumentFontFamily,
				},
				FontSize = new FontSize { Val = "36" }
			}
		};
		titleParagraph.Append(titleRun);
		titleParagraph.ParagraphProperties = new ParagraphProperties(new Justification() { Val = JustificationValues.Center });

		Table table = new();
		var type = typeof(T);
		var columnsTitles = type.GetProperties().Select(e => e.Name);

		var headerRow = new TableRow();
		foreach (var item in columnsTitles)
        {
			headerRow.Append(CreateTableCell(item, docxDocumentOptions));
		}
		table.Append(headerRow);

		return (table, titleParagraph);
	}

	private static void FillTable<T>(Table table, IEnumerable<T> itemsToFill, DocxDocumentOptions docxDocumentOptions)
	{
		var itemsType = typeof(T);
		foreach (var item in itemsToFill)
		{
			TableRow dataRow = new();
            foreach (var property in itemsType.GetProperties())
            {
                object? value = property.GetValue(item);
				dataRow.Append(CreateTableCell(value as string, docxDocumentOptions));
            }
            table.Append(dataRow);
		}
	}

	private static TableCell CreateTableCell(string text, DocxDocumentOptions docxDocumentOptions)
	{
		var cell = new TableCell(new Paragraph(new Run(new Text(text)) { RunProperties = new RunProperties 
		{ RunFonts = new RunFonts { Ascii = docxDocumentOptions.DocumentFontFamily }, FontSize = new FontSize { Val = docxDocumentOptions.DocumentFontSize.ToString() } } }))
		{
			TableCellProperties = new TableCellProperties(
			new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2400" },
			new TableCellBorders(
				new TopBorder() { Val = BorderValues.Single, Size = 8, Color = "000000" },
				new BottomBorder() { Val = BorderValues.Single, Size = 8, Color = "000000" },
				new LeftBorder() { Val = BorderValues.Single, Size = 8, Color = "000000" },
				new RightBorder() { Val = BorderValues.Single, Size = 8, Color = "000000" }
			)
		)};


		cell.TableCellProperties.Append(new TableCellMargin() 
		{ 
			LeftMargin = new LeftMargin() 
			{ 
				Width = "100" 
			} 
		});
		return cell;
	}
}

public class DocxDocumentOptions
{
	public required string DocumentFontFamily { get; init; }

	public required int DocumentFontSize { get; init; }
}