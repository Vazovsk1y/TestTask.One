namespace TestTaskOne.Core.Models;

#nullable disable
public class WaybillItem
{
    public Guid WaybillId { get; set; }

    public Waybill Waybill { get; set; }

    public Guid NomenclatureId { get; set; }

    public Nomenclature Nomenclature { get; set; }
}
