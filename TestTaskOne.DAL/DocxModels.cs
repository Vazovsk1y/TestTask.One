namespace TestTaskOne.DAL;

internal static class DocxModels
{
#nullable disable
	internal class ProductDocxModel
	{
		public string Title { get; set; }

		public string ElementsUsed { get; set; }
	}

	internal abstract class NomenclatureDocxModel
	{
		public string Title { get; set; }
	}

	internal class MaterialDocxModel : NomenclatureDocxModel
	{

	}

	internal class ComponentDocxModel : NomenclatureDocxModel
	{

	}

	internal class WaybillDocxModel
	{
		public string Id { get; set; }

		public string PurchaseCost { get; set; }

		public string PurchaseDate { get; set; }

		public string PurchaseItems { get; set; }
	}
}
