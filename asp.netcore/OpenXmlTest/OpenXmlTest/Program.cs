using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;


class Program
{
    static void Main(string[] args)
    {
        string filePath = "Inventory.xlsx";
        var data = ReadExcelFile(filePath);

        foreach (var row in data)
        {
            Console.WriteLine(string.Join(", ", row));
        }
    }

    static List<List<string>> ReadExcelFile(string filePath)
    {
        var result = new List<List<string>>();

        using (SpreadsheetDocument document = SpreadsheetDocument.Open(filePath, false))
        {
            WorkbookPart workbookPart = document.WorkbookPart;
            Sheet sheet = workbookPart.Workbook.Descendants<Sheet>().FirstOrDefault();
            WorksheetPart worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id);
            SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

            foreach (Row row in sheetData.Elements<Row>().Skip(6))
            {
                var rowData = new List<string>();
                foreach (Cell cell in row.Elements<Cell>())
                {
                    rowData.Add(GetCellValue(cell, workbookPart));
                }
                result.Add(rowData);
            }
        }

        return result;
    }

    static string GetCellValue(Cell cell, WorkbookPart workbookPart)
    {
        string value = cell.InnerText;
        if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
        {
            var stringTable = workbookPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault();
            if (stringTable != null)
            {
                value = stringTable.SharedStringTable.ElementAt(int.Parse(value)).InnerText;
            }
        }
        return value;
    }
}