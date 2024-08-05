using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using StateTaxService.AIAT.Inventory.Helpers;
using StateTaxService.AIAT.Inventory.Models;

namespace StateTaxService.AIAT.Inventory.Services.ExcelValidation;

public class ValidateService : IValidateService
{
    private readonly IHardSoftCheckService hardSoftCheckService;
    private readonly IDataTypesCheckService dataTypesCheckService;

    public ValidateService(IHardSoftCheckService hardSoftCheckService, IDataTypesCheckService dataTypesCheckService)
    {
        this.hardSoftCheckService = hardSoftCheckService;
        this.dataTypesCheckService = dataTypesCheckService;
    }
    public List<InventoryRowValidationResult> ValidateHardAndSoftErrors(byte[] fileData)
    {
        var allRows = new List<InventoryRowValidationResult>();

        foreach (var rowData in GetRows(fileData))
        {
            var validationResult = hardSoftCheckService.HardAndSoftCheck(rowData);
            bool hasErrors = validationResult.HardCheckErrors.Any() || validationResult.SoftCheckErrors.Any();

            if (hasErrors)
                allRows.Add(validationResult);
        }
        return allRows;
    }

    public List<InventoryRowValidationResult> ValidateDataTypes(byte[] fileData)
    {
        var allRows = new List<InventoryRowValidationResult>();

        foreach (var rowData in GetRows(fileData))
        {
            var validationResult = dataTypesCheckService.CheckDataTypes(rowData);
            bool hasErrors = validationResult.HardCheckErrors.Any();

            if (hasErrors)
                allRows.Add(validationResult);
        }
        return allRows;
    }

    public List<UploadedExcelDataRow> GetData(byte[] fileData)
    {
        var allRows = new List<UploadedExcelDataRow>();

        foreach (var rowData in GetRows(fileData))
        {
            UploadedExcelDataRow rowResponse = new UploadedExcelDataRow { RowNumber = rowData.RowIndex };
            int endCol = InventoryConstantDefaultValues.dataColumnEnd;
            for (int i = 1; i <= endCol; i++)
            {
                string cellValue = rowData.Cells[i]?.Value;
                rowResponse.Values[i] = cellValue;
            }
            allRows.Add(rowResponse);
        }        
        return allRows;
    }

    private IEnumerable<RowData> GetRows(byte[] fileData) 
    {
        using var stream = new MemoryStream(fileData);
        using SpreadsheetDocument document = SpreadsheetDocument.Open(stream, false);
        WorkbookPart workbookPart = document.WorkbookPart;
        SheetData sheetData = GetFirstSheetData(workbookPart);
        int maxColumnCount = GetMaxColumnCount(sheetData);
        foreach (Row row in sheetData.Elements<Row>().Skip(6))
        {
            yield return ReadRowValues(row, maxColumnCount, workbookPart);
        }
    }

    private SheetData GetFirstSheetData(WorkbookPart workbookPart)
    {
        return workbookPart.WorksheetParts.First().Worksheet.GetFirstChild<SheetData>();
    }

    private int GetMaxColumnCount(SheetData sheetData)
    {
        return sheetData.Elements<Row>()
                        .Max(row => row.Elements<Cell>()
                                      .Max(cell => GetColumnIndexFromCellReference(cell.CellReference))) + 1;
    }

    private RowData ReadRowValues(Row row, int maxColumnIndex, WorkbookPart workbookPart)
    {
        List<CellData> cells = new List<CellData>(new CellData[maxColumnIndex + 1]);
        foreach (Cell cell in row.Elements<Cell>())
        {
            int cellIndex = GetColumnIndexFromCellReference(cell.CellReference);
            string cellValue = GetCellValue(cell, workbookPart);
            cells[cellIndex + 1] = new CellData
            {
                ColumnIndex = cellIndex + 1,
                Value = cellValue
            };
        }
        return new RowData
        {
            RowIndex = (int)row.RowIndex.Value,
            Cells = cells
        };
    }

    private int GetColumnIndexFromCellReference(string cellReference)
    {
        return GetColumnIndexFromName(GetColumnNameFromReference(cellReference));
    }

    private int GetColumnIndexFromName(string columnName)
    {
        int columnIndex = 0;
        int alphabetCount = 26;
        foreach (char ch in columnName)
        {
            columnIndex = columnIndex * alphabetCount + (ch - 'A' + 1);
        }
        return columnIndex - 1;
    }

    private string GetColumnNameFromReference(string cellReference)
    {
        return new string(cellReference.TakeWhile(c => !char.IsDigit(c)).ToArray());
    }

    private string GetCellValue(Cell cell, WorkbookPart workbookPart)
    {
        if (cell == null)
        {
            return null;
        }

        string value = cell.InnerText;

        if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
        {
            return workbookPart.SharedStringTablePart.SharedStringTable
                              .ElementAt(int.Parse(value)).InnerText;
        }

        return value;
    }
}
