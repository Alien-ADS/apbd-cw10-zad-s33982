namespace Cwiczenia_10.Entities;

public class PCComponents {
    public int PCId { get; set; }
    public string ComponentCode { get; set; } = string.Empty;
    public int Amount { get; set; }
    
    public PCs PCs { get; set; }
    public Components Components { get; set; }
}