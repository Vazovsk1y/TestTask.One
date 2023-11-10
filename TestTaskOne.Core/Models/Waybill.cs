namespace TestTaskOne.Core.Models;

// Накладная
public class Waybill
{
    public Guid Id { get; }

    public double PurchaseCost { get; set; }

    public DateTime PurchaseDate { get; set; }

    public Waybill()
    {
        Id = Guid.NewGuid();
    }
}
