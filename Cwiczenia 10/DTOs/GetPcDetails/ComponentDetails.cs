namespace Cwiczenia_10.DTOs.GetPcDetails;

public class ComponentDetails {
    public string Code { get; set; } = "";
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public ManufacturerDetails Manufacturer { get; set; }
    public TypeDetails Type { get; set; }
}