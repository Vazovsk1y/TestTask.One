namespace TestTaskOne.Core.Models;

public class WaybillItem
{
    public Guid WaybillId { get; }

    public Waybill Waybill { get; set; }

    public Guid NomenclatureId { get; }

    public Nomenclature Nomenclature { get; set; }

    public WaybillItem(Guid waybillId, Guid nomenclatureId)
    {
        WaybillId = waybillId;
        NomenclatureId = nomenclatureId;
    }
}
