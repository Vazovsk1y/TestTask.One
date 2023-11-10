namespace TestTaskOne.Core.Models;

public class NomenclatureLink
{
    public Guid NomeclatureId { get; }

    public Nomenclature Nomenclature { get; set; }

    public Guid ProductId { get; }

    public Nomenclature Product { get; set; }

    public NomenclatureLink(Guid nomeclatureId, Guid productId)
    {
        NomeclatureId = nomeclatureId;
        ProductId = productId;
    }
}
