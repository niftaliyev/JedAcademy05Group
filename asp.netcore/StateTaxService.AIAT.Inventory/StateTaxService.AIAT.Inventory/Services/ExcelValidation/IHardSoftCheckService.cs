using StateTaxService.AIAT.Inventory.Models;

namespace StateTaxService.AIAT.Inventory.Services.ExcelValidation;

public interface IHardSoftCheckService
{
    InventoryRowValidationResult HardAndSoftCheck(RowData rowData);
}
