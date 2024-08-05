using StateTaxService.AIAT.Inventory.Helpers;
using StateTaxService.AIAT.Inventory.Models;

namespace StateTaxService.AIAT.Inventory.Services.ExcelValidation;

public class HardSoftCheckService : IHardSoftCheckService
{
    public InventoryRowValidationResult HardAndSoftCheck(RowData rowData)
    {
        var hardErrors = HardChecks(rowData, rowData.RowIndex);
        var softErrors = SoftChecks(rowData, rowData.RowIndex);

        InventoryRowValidationResult inventoryRow = new InventoryRowValidationResult { RowNumber = rowData.RowIndex };

        if (hardErrors.Any())
            inventoryRow.HardCheckErrors.AddRange(hardErrors);

        if (softErrors.Any())
            inventoryRow.SoftCheckErrors.AddRange(softErrors);

        return inventoryRow;
    }
    private List<ValidationErrorDTOModel> HardChecks(RowData rowData, int row)
    {
        int endCol = InventoryConstantDefaultValues.dataColumnEnd;
        List<ValidationErrorDTOModel> hardErrors = new List<ValidationErrorDTOModel>();
        for (int col = 1; col <= endCol; col++)
        {
            string cellValue = rowData.Cells[col]?.Value;
            if (col == 2 && string.IsNullOrWhiteSpace(cellValue))
                hardErrors.Add(new ValidationErrorDTOModel { column = col, error = HardCheckConstants.ProductNameIsNull });

            if (col == 3 && string.IsNullOrEmpty(cellValue))
                hardErrors.Add(new ValidationErrorDTOModel { column = col, error = HardCheckConstants.UnitIsNull });

            if ((col == 11 || col == 12) && string.IsNullOrWhiteSpace(rowData.Cells[10]?.Value))
            {
                hardErrors.Add(new ValidationErrorDTOModel
                {
                    column = col,
                    error = HardCheckConstants.IfColumn11OrColumn12IsPresentThenColumn10ShouldBePresent
                });
            }
            if (col == 13 && !string.IsNullOrWhiteSpace(cellValue))
            {
                double? column13Value = CheckValueHelper.IsNumeric(cellValue).Value;
                double? column14Value = CheckValueHelper.IsNumeric(rowData.Cells[14]?.Value).Value;
                double? column15Value = CheckValueHelper.IsNumeric(rowData.Cells[15]?.Value).Value;

                if (column13Value.HasValue && column14Value.HasValue && column15Value.HasValue)
                {
                    double calculatedValue = column13Value.Value * column14Value.Value;
                    double difference = Math.Abs((calculatedValue - column15Value.Value) / column15Value.Value);

                    if (difference >= 0.01)
                    {
                        hardErrors.Add(new ValidationErrorDTOModel
                        {
                            column = col,
                            error = HardCheckConstants.ForEachRowIfColumn13Column14AndColumn15ArePresentThenAbsoluteLessThan1Percent
                        });
                    }
                }
            }
            if ((col == 17 || col == 18) && string.IsNullOrWhiteSpace(rowData.Cells[16]?.Value))
            {
                hardErrors.Add(new ValidationErrorDTOModel
                {
                    column = col,
                    error = HardCheckConstants.IfColumn17OrColumn18IsPresentThenColumn16ShouldBePresent
                });
            }
            if ((col == 20 || col == 21) && string.IsNullOrWhiteSpace(rowData.Cells[19]?.Value))
            {
                hardErrors.Add(new ValidationErrorDTOModel
                {
                    column = col,
                    error = HardCheckConstants.IfColumn20OrColumn21IsPresentThenColumn19ShouldBePresent
                });
            }
            if ((col == 35 || col == 36) && string.IsNullOrWhiteSpace(rowData.Cells[34]?.Value))
            {
                hardErrors.Add(new ValidationErrorDTOModel
                {
                    column = col,
                    error = HardCheckConstants.IfColumn35OrColumn36IsPresentThenColumn34ShouldBePresent
                });
            }
            if ((col == 23 || col == 24) && string.IsNullOrWhiteSpace(rowData.Cells[22]?.Value))
            {
                hardErrors.Add(new ValidationErrorDTOModel
                {
                    column = col,
                    error = HardCheckConstants.IfColumn23OrColumn24IsPresentThenColumn22ShouldBePresent
                });
            }
            if ((col == 26 || col == 27) && string.IsNullOrWhiteSpace(rowData.Cells[26]?.Value))
            {
                hardErrors.Add(new ValidationErrorDTOModel
                {
                    column = col,
                    error = HardCheckConstants.IfColumn26OrColumn27IsPresentThenColumn26ShouldBePresent
                });
            }
            if ((col == 29 || col == 30) && string.IsNullOrWhiteSpace(rowData.Cells[28]?.Value))
            {
                hardErrors.Add(new ValidationErrorDTOModel
                {
                    column = col,
                    error = HardCheckConstants.IfColumn29OrColumn30IsPresentThenColumn28ShouldBePresent
                });
            }
            if ((col == 32 || col == 33) && string.IsNullOrWhiteSpace(rowData.Cells[31]?.Value))
            {
                hardErrors.Add(new ValidationErrorDTOModel
                {
                    column = col,
                    error = HardCheckConstants.IfColumn32OrColumn33IsPresentThenColumn31ShouldBePresent
                });
            }
        }
        return hardErrors;
    }
    private List<ValidationErrorDTOModel> SoftChecks(RowData rowData, int row)
    {
        List<ValidationErrorDTOModel> softErrors = new List<ValidationErrorDTOModel>();
        bool hasValueInColumns4To36 = false;

        int endCol = InventoryConstantDefaultValues.dataColumnEnd;

        for (int col = 1; col <= endCol; col++)
        {
            string cellValue = rowData.Cells[col]?.Value;
            if (col >= 4 && col <= endCol && !string.IsNullOrWhiteSpace(cellValue))
                hasValueInColumns4To36 = true;

            if (col == 4 && !string.IsNullOrWhiteSpace(cellValue))
            {
                double? column4Value = CheckValueHelper.IsNumeric(cellValue).Value;
                double? column5Value = CheckValueHelper.IsNumeric(rowData.Cells[5]?.Value).Value;
                double? column6Value = CheckValueHelper.IsNumeric(rowData.Cells[6]?.Value).Value;

                if (!column5Value.HasValue && !column6Value.HasValue)
                {
                    softErrors.Add(new ValidationErrorDTOModel
                    {
                        column = col,
                        error = SoftCheckConstants.IfColumn4IsPresentThenAtLeastOneOfColumn5OrColumn6ShouldBePresent
                    });
                }

                if (column5Value.HasValue && column6Value.HasValue && column4Value.HasValue)
                {
                    double calculatedValue = column4Value.Value * column5Value.Value;
                    double difference = Math.Abs((calculatedValue - column6Value.Value) / column6Value.Value);

                    if (difference >= 0.01)
                    {
                        softErrors.Add(new ValidationErrorDTOModel
                        {
                            column = col,
                            error = SoftCheckConstants.ForEachRowIfColumn4Column5AndColumn6ArePresentThenAbsoluteLessThan1Percent
                        });
                    }
                }
            }
            if (col == 7 && !string.IsNullOrWhiteSpace(cellValue))
            {
                double? isColumn8Present = CheckValueHelper.IsNumeric(rowData.Cells[8]?.Value).Value;
                double? isColumn9Present = CheckValueHelper.IsNumeric(rowData.Cells[9]?.Value).Value;

                if (!isColumn8Present.HasValue && !isColumn9Present.HasValue)
                {
                    softErrors.Add(new ValidationErrorDTOModel
                    {
                        column = col,
                        error = SoftCheckConstants.IfColumn7IsPresentThenAtLeastOneOfColumn8OrColumn9ShouldBePresent
                    });
                }
            }
            if (col == 7 && !string.IsNullOrWhiteSpace(cellValue))
            {
                double? column7Value = CheckValueHelper.IsNumeric(cellValue).Value;
                double? column8Value = CheckValueHelper.IsNumeric(rowData.Cells[8]?.Value).Value;
                double? column9Value = CheckValueHelper.IsNumeric(rowData.Cells[9]?.Value).Value;

                if (column7Value.HasValue && column8Value.HasValue && column9Value.HasValue)
                {
                    double calculatedValue = column7Value.Value * column8Value.Value;
                    double difference = Math.Abs((calculatedValue - column9Value.Value) / column9Value.Value);

                    if (difference >= 0.01)
                    {
                        softErrors.Add(new ValidationErrorDTOModel
                        {
                            column = col,
                            error = SoftCheckConstants.ForEachRowIfColumn7Column8AndColumn9ArePresentThenAbsoluteLessThan1Percent
                        });
                    }
                }
            }
            if ((col == 8 || col == 9) && string.IsNullOrWhiteSpace(rowData.Cells[7]?.Value))
            {
                softErrors.Add(new ValidationErrorDTOModel
                {
                    column = col,
                    error = SoftCheckConstants.IfColumn8OrColumn9IsPresentThenColumn7ShouldBePresent
                });
            }
            if (col == 10 && !string.IsNullOrWhiteSpace(cellValue))
            {
                double? isColumn11Present = CheckValueHelper.IsNumeric(rowData.Cells[11]?.Value).Value;
                double? isColumn12Present = CheckValueHelper.IsNumeric(rowData.Cells[12]?.Value).Value;

                if (!isColumn11Present.HasValue && !isColumn12Present.HasValue)
                {
                    softErrors.Add(new ValidationErrorDTOModel
                    {
                        column = col,
                        error = SoftCheckConstants.IfColumn10IsPresentThenAtLeastOneOfColumn11OrColumn12ShouldBePresent
                    });
                }
            }
            if (col == 10 && !string.IsNullOrWhiteSpace(cellValue))
            {
                double? column10Value = CheckValueHelper.IsNumeric(cellValue).Value;
                double? column11Value = CheckValueHelper.IsNumeric(rowData.Cells[11]?.Value).Value;
                double? column12Value = CheckValueHelper.IsNumeric(rowData.Cells[12]?.Value).Value;

                if (column10Value.HasValue && column11Value.HasValue && column12Value.HasValue)
                {
                    double calculatedValue = column10Value.Value * column11Value.Value;
                    double difference = Math.Abs((calculatedValue - column12Value.Value) / column12Value.Value);

                    if (difference >= 0.01)
                    {
                        softErrors.Add(new ValidationErrorDTOModel
                        {
                            column = col,
                            error = SoftCheckConstants.ForEachRowIfColumn10Column11AndColumn12ArePresentThenAbsoluteLessThan1Percent
                        });
                    }
                }
            }
            if (col == 13 && !string.IsNullOrWhiteSpace(cellValue))
            {
                double? isColumn14Present = CheckValueHelper.IsNumeric(rowData.Cells[14]?.Value).Value;
                double? isColumn15Present = CheckValueHelper.IsNumeric(rowData.Cells[15]?.Value).Value;

                if (!isColumn14Present.HasValue && !isColumn15Present.HasValue)
                {
                    softErrors.Add(new ValidationErrorDTOModel
                    {
                        column = col,
                        error = SoftCheckConstants.IfColumn13IsPresentThenAtLeastOneOfColumn14OrColumn15ShouldBePresent
                    });
                }
            }
            if (col == 16 && !string.IsNullOrWhiteSpace(cellValue))
            {
                double? isColumn17Present = CheckValueHelper.IsNumeric(rowData.Cells[17]?.Value).Value;
                double? isColumn18Present = CheckValueHelper.IsNumeric(rowData.Cells[18]?.Value).Value;

                if (!isColumn17Present.HasValue && !isColumn18Present.HasValue)
                {
                    softErrors.Add(new ValidationErrorDTOModel
                    {
                        column = col,
                        error = SoftCheckConstants.IfColumn16IsPresentThenAtLeastOneOfColumn17OrColumn18ShouldBePresent
                    });
                }
            }
            if (col == 16 && !string.IsNullOrWhiteSpace(cellValue))
            {
                double? column16Value = CheckValueHelper.IsNumeric(cellValue).Value;
                double? column17Value = CheckValueHelper.IsNumeric(rowData.Cells[17]?.Value).Value;
                double? column18Value = CheckValueHelper.IsNumeric(rowData.Cells[18]?.Value).Value;

                if (column16Value.HasValue && column17Value.HasValue && column18Value.HasValue)
                {
                    double product = column16Value.Value * column17Value.Value;
                    double percentDiff = Math.Abs((product - column18Value.Value) / column18Value.Value);

                    if (percentDiff >= 0.01)
                    {
                        softErrors.Add(new ValidationErrorDTOModel
                        {
                            column = col,
                            error = SoftCheckConstants.ForEachRowIfColumn16Column17AndColumn18ArePresentThenAbsoluteLessThan1Percent
                        });
                    }
                }
            }
            if (col == 19 && !string.IsNullOrWhiteSpace(cellValue))
            {
                bool isColumn20Present = !string.IsNullOrWhiteSpace(rowData.Cells[20]?.Value);
                bool isColumn21Present = !string.IsNullOrWhiteSpace(rowData.Cells[21]?.Value);

                if (!isColumn20Present && !isColumn21Present)
                {
                    softErrors.Add(new ValidationErrorDTOModel
                    {
                        column = col,
                        error = SoftCheckConstants.IfColumn19IsPresentThenAtLeastOneOfColumn20OrColumn21ShouldBePresent
                    });
                }
            }
            if (col == 19 && !string.IsNullOrWhiteSpace(cellValue))
            {
                double? column19Value = CheckValueHelper.IsNumeric(cellValue).Value;
                double? column20Value = CheckValueHelper.IsNumeric(rowData.Cells[20]?.Value).Value;
                double? column21Value = CheckValueHelper.IsNumeric(rowData.Cells[21]?.Value).Value;

                if (column19Value.HasValue && column20Value.HasValue && column21Value.HasValue)
                {
                    double product = column19Value.Value * column20Value.Value;
                    double percentDiff = Math.Abs((product - column21Value.Value) / column21Value.Value);

                    if (percentDiff >= 0.01)
                    {
                        softErrors.Add(new ValidationErrorDTOModel
                        {
                            column = col,
                            error = SoftCheckConstants.ForEachRowIfColumn19Column20AndColumn21ArePresentThenAbsoluteLessThan1Percent
                        });
                    }
                }
            }
            if (col == 34 && !string.IsNullOrWhiteSpace(cellValue))
            {
                bool isColumn35Present = !string.IsNullOrWhiteSpace(rowData.Cells[35]?.Value);
                bool isColumn36Present = !string.IsNullOrWhiteSpace(rowData.Cells[36]?.Value);

                if (!isColumn35Present && !isColumn36Present)
                {
                    softErrors.Add(new ValidationErrorDTOModel
                    {
                        column = col,
                        error = SoftCheckConstants.IfColumn34IsPresentThenAtLeastOneOfColumn35OrColumn36ShouldBePresent
                    });
                }
            }
            if (col == 34 && !string.IsNullOrWhiteSpace(cellValue))
            {
                double? column34Value = CheckValueHelper.IsNumeric(cellValue).Value;
                double? column35Value = CheckValueHelper.IsNumeric(rowData.Cells[35]?.Value).Value;
                double? column36Value = CheckValueHelper.IsNumeric(rowData.Cells[36]?.Value).Value;

                if (column34Value.HasValue && column35Value.HasValue && column36Value.HasValue)
                {
                    double product = column34Value.Value * column35Value.Value;
                    double percentDiff = Math.Abs((product - column36Value.Value) / column36Value.Value);

                    if (percentDiff >= 0.01)
                    {
                        softErrors.Add(new ValidationErrorDTOModel
                        {
                            column = col,
                            error = SoftCheckConstants.ForEachRowIfColumn34Column35AndColumn36ArePresentThenAbsoluteLessThan1Percent
                        });
                    }
                }
            }
            if (col == 22 && !string.IsNullOrWhiteSpace(cellValue))
            {
                bool isColumn23Present = !string.IsNullOrWhiteSpace(rowData.Cells[23]?.Value);
                bool isColumn24Present = !string.IsNullOrWhiteSpace(rowData.Cells[24]?.Value);

                if (!isColumn23Present || !isColumn24Present)
                {
                    softErrors.Add(new ValidationErrorDTOModel
                    {
                        column = col,
                        error = SoftCheckConstants.IfColumn22IsPresentThenBothColumn23AndColumn24ShouldBePresent
                    });
                }
            }
            if (col == 25 && !string.IsNullOrWhiteSpace(cellValue))
            {
                bool isColumn26Present = !string.IsNullOrWhiteSpace(rowData.Cells[26]?.Value);
                bool isColumn27Present = !string.IsNullOrWhiteSpace(rowData.Cells[27]?.Value);

                if (!isColumn26Present || !isColumn27Present)
                {
                    softErrors.Add(new ValidationErrorDTOModel
                    {
                        column = col,
                        error = SoftCheckConstants.IfColumn25IsPresentThenBothColumn26AndColumn27ShouldBePresent
                    });
                }
            }
            if (col == 28 && !string.IsNullOrWhiteSpace(cellValue))
            {
                bool isColumn29Present = !string.IsNullOrWhiteSpace(rowData.Cells[29]?.Value);
                bool isColumn30Present = !string.IsNullOrWhiteSpace(rowData.Cells[30]?.Value);

                if (!isColumn29Present || !isColumn30Present)
                {
                    softErrors.Add(new ValidationErrorDTOModel
                    {
                        column = col,
                        error = SoftCheckConstants.IfColumn28IsPresentThenBothColumn29AndColumn30ShouldBePresent
                    });
                }
            }
            if (col == 31 && !string.IsNullOrWhiteSpace(cellValue))
            {
                bool isColumn32Present = !string.IsNullOrWhiteSpace(rowData.Cells[32]?.Value);
                bool isColumn33Present = !string.IsNullOrWhiteSpace(rowData.Cells[33]?.Value);

                if (!isColumn32Present || !isColumn33Present)
                {
                    softErrors.Add(new ValidationErrorDTOModel
                    {
                        column = col,
                        error = SoftCheckConstants.IfColumn31IsPresentThenBothColumn32AndColumn33ShouldBePresent
                    });
                }
            }
            if (col == endCol)
            {
                bool isColumn2Present = !string.IsNullOrWhiteSpace(rowData.Cells[2]?.Value);
                bool isColumn3Present = !string.IsNullOrWhiteSpace(rowData.Cells[3]?.Value);

                if (isColumn2Present && isColumn3Present && !hasValueInColumns4To36)
                {
                    softErrors.Add(new ValidationErrorDTOModel
                    {
                        column = col,
                        error = SoftCheckConstants.IfColumn2AndColumn3ArePresentAtLeastOneOfTheRemainingColumnsFromColumn4ToColumn36ShouldBePresent
                    });
                }
            }
        }
        return softErrors;
    }
}
