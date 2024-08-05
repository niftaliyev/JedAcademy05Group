namespace StateTaxService.AIAT.Inventory.Models;

public record ValidationErrorDTOModel
{
    public int column { get; set; }
    public string? error { get; set; }
}
