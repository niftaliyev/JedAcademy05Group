namespace StateTaxService.AIAT.Inventory.Models;

public class UploadAndValidateExcelResponseModel
{
    public Guid FileId { get; set; }
    public List<InventoryRowValidationResult> Result { get; set; }
}
