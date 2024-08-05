using StateTaxService.AIAT.Inventory.Helpers;
using StateTaxService.AIAT.Inventory.Models;

namespace StateTaxService.AIAT.Inventory.Services.ExcelValidation;
public class DataTypesCheckService : IDataTypesCheckService
{
    public InventoryRowValidationResult CheckDataTypes(RowData rowData)
    {
        InventoryRowValidationResult inventoryRow = new InventoryRowValidationResult { RowNumber = rowData.RowIndex };
        for (int i = 1; i < rowData.Cells.Count-1; i++)
        {
            string cellValue = rowData.Cells[i]?.Value;
            var error = CheckCellValue(i, cellValue);
            if (error != null)
                inventoryRow.HardCheckErrors.Add(error);
        }

        return inventoryRow;
    }
    private ValidationErrorDTOModel? CheckCellValue(int col, string cellValue)
    {
        int productNameColumnIndex = 2;
        if (col == productNameColumnIndex && string.IsNullOrWhiteSpace(cellValue))
        {
            return new ValidationErrorDTOModel { column = col, error = $"'{cellValue}' - {HardCheckConstants.ShouldNotEmpty}" };
        }
        if (col != productNameColumnIndex && !string.IsNullOrEmpty(cellValue) && !CheckValueHelper.IsNumeric(cellValue).isNumber)
        {
            return new ValidationErrorDTOModel { column = col, error = $"'{cellValue}' - {HardCheckConstants.IsNotANumber}" };
        }
        return null;
    }
}