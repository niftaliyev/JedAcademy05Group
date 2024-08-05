namespace StateTaxService.AIAT.Inventory.Models;

public record CellData
{
    public int? ColumnIndex { get; set; }
    public string Value { get; set; }
}