namespace TestTaskOne.Core.Models;

/// <summary>
/// Накладная
/// </summary>
public class Waybill
{
    public Guid Id { get; }

    public double PurchaseCost { get; set; }

    public DateTime PurchaseDate { get; set; }

    public ICollection<WaybillItem> PurchaseItems { get; set; } = new HashSet<WaybillItem>();

    public Waybill()
    {
        Id = Guid.NewGuid();
    }
}
