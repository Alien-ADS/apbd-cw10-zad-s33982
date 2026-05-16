namespace Cwiczenia_10.Entities;

public class ComponentManufacturers {
    public int Id { get; set; }
    public string Abbreviation { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public DateTime FoundationDate { get; set; }
    
    public ICollection<Components> Components { get; set; } = [];
}