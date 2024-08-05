using StateTaxService.AIAT.Inventory.Models;

namespace StateTaxService.AIAT.Inventory.Services.ExcelValidation;

public interface IDataTypesCheckService
{
    InventoryRowValidationResult CheckDataTypes(RowData rowData);
}
