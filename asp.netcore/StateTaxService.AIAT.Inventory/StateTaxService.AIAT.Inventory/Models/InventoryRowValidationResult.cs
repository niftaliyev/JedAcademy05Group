namespace StateTaxService.AIAT.Inventory.Models;

public record class InventoryRowValidationResult
{
    public int RowNumber { get; set; }
    public List<ValidationErrorDTOModel> HardCheckErrors { get; set; } = new List<ValidationErrorDTOModel>();
    public List<ValidationErrorDTOModel> SoftCheckErrors { get; set; } = new List<ValidationErrorDTOModel>();
}
