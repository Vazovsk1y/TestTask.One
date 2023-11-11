namespace TestTaskOne.Core.Models;

#nullable disable
public class Product
{
    public Guid Id { get; }

    public string Title { get; set; }

    public ICollection<NomenclatureLink> ElementsUsed { get; set; } = new HashSet<NomenclatureLink>();

    public Product()
    {
        Id = Guid.NewGuid();
    }
}
