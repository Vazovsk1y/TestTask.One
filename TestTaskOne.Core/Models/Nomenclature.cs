namespace TestTaskOne.Core.Models;
#nullable disable

public abstract class Nomenclature
{
    public Guid Id { get; }

    public string Title { get; set; }

    public Nomenclature()
    {
        Id = Guid.NewGuid();
    }
}
