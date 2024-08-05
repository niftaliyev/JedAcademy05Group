using StateTaxService.AIAT.Inventory.Controllers;

namespace StateTaxService.AIAT.Inventory.Models;

public record RowData
{
    public int RowIndex { get; set; }
    public List<CellData> Cells { get; set; }
}
