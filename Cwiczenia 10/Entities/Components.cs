namespace Cwiczenia_10.Entities;

public class Components {
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ComponentManufacturersId { get; set; }
    public int ComponentTypesId { get; set; }
    
    public ComponentManufacturers ComponentManufacturers { get; set; }
    public ComponentTypes ComponentType { get; set; }
    
    public ICollection<PCComponents> PCComponents { get; set; } = [];
}