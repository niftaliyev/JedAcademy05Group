using StateTaxService.AIAT.Inventory.Models;

namespace StateTaxService.AIAT.Inventory.Services.ExcelValidation;

public interface IValidateService
{
    List<UploadedExcelDataRow> GetData(byte[] fileData);
    List<InventoryRowValidationResult> ValidateDataTypes(byte[] fileData);
    List<InventoryRowValidationResult> ValidateHardAndSoftErrors(byte[] fileData);
}
