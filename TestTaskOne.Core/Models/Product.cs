namespace TestTaskOne.Core.Models;

public class Product
{
    public Guid Id { get; }

    public ICollection<NomenclatureLink> ElementsUsed { get; set; } = new HashSet<NomenclatureLink>();

    public Product()
    {
        Id = Guid.NewGuid();
    }
}
