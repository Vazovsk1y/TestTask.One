namespace TestTaskOne.Core.Models;

#nullable disable
public class NomenclatureLink
{
    public Guid NomenclatureId { get; set; }

    public Nomenclature Nomenclature { get; set; }

    public Guid ProductId { get; set; }

    public Product Product { get; set; }
}
