namespace StateTaxService.AIAT.Inventory.Helpers;

public static class CheckValueHelper
{
    public static (bool isNumber, double? Value) IsNumeric(string input)
    {
        bool isDouble = double.TryParse(input, out double result);
        return (isDouble, isDouble ? result : null);
    }
}
