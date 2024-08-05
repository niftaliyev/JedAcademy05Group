namespace StateTaxService.AIAT.Inventory.Models;

public record class UploadedExcelDataRow
{
    public int RowNumber { get; set; }
    public Dictionary<int, string> Values { get; set; } = new();
}
