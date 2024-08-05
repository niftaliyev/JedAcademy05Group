namespace StateTaxService.AIAT.Inventory.Models;

public static class SoftCheckConstants
{
    public const string IfColumn4IsPresentThenAtLeastOneOfColumn5OrColumn6ShouldBePresent = "Əgər sütun 4 varsa, o zaman sütun5 və ya sütun6-dan ən azı biri mövcud olmalıdır.";
    public const string ForEachRowIfColumn4Column5AndColumn6ArePresentThenAbsoluteLessThan1Percent = "Əgər sütun4, sütun5 və sütun6 mövcuddursa, mütləq (fərq(sütun4*sütun5 - sütun6)/sütun6) 1 faizdən kiçik olmalıdır.";
    public const string IfColumn2AndColumn3ArePresentAtLeastOneOfTheRemainingColumnsFromColumn4ToColumn36ShouldBePresent = "Sütun 2 və sütun 3 varsa, sütun 4-dən 36-cı sütuna qədər qalan sütunlardan ən azı biri mövcud olmalıdır.";
    public const string IfColumn7IsPresentThenAtLeastOneOfColumn8OrColumn9ShouldBePresent = "Əgər sütun 7 varsa, o zaman sütun 8 və ya 9-cu sütunlardan ən azı biri mövcud olmalıdır.";
    public const string ForEachRowIfColumn7Column8AndColumn9ArePresentThenAbsoluteLessThan1Percent = "Əgər sütun 7 sütun 8 və sütun 9 varsa, mütləq (fərq(sütun7*sütun8 - sütun9)/sütun9) 1 faizdən kiçik olmalıdır.";
    public const string IfColumn8OrColumn9IsPresentThenColumn7ShouldBePresent = "Əgər sütun8 və ya 9-cu sütundan biri varsa, o zaman sütun7 mövcud olmalıdır.";
    public const string IfColumn10IsPresentThenAtLeastOneOfColumn11OrColumn12ShouldBePresent = "Hər bir sətir üçün 10-cu sütun varsa, ən azı 11-ci və ya 12-ci sütundan biri olmalıdır.";
    public const string ForEachRowIfColumn10Column11AndColumn12ArePresentThenAbsoluteLessThan1Percent = "Əgər sütun 10 sütun 11 və sütun 12 mövcuddursa, mütləq (fərq(sütun10*sütun11 - sütun12)/sütun12) 1 faizdən kiçik olmalıdır.";
    public const string IfColumn13IsPresentThenAtLeastOneOfColumn14OrColumn15ShouldBePresent = "Əgər sütun 13 varsa, o zaman sütun 14 və ya sütun 15-dən ən azı biri mövcud olmalıdır.";
    public const string IfColumn16IsPresentThenAtLeastOneOfColumn17OrColumn18ShouldBePresent = "Əgər sütun16 varsa, o zaman sütun17 və ya sütun18-dən ən azı biri mövcud olmalıdır.";
    public const string ForEachRowIfColumn16Column17AndColumn18ArePresentThenAbsoluteLessThan1Percent = "Əgər sütun 16 sütun 17 və sütun 18 varsa, mütləq (fərq(sütun16*sütun17 - sütun18)/sütun18) 1 faizdən kiçik olmalıdır.";
    public const string IfColumn19IsPresentThenAtLeastOneOfColumn20OrColumn21ShouldBePresent = "Əgər sütun 19 varsa, o zaman sütun 20 və ya sütun 21-dən ən azı biri mövcud olmalıdır.";
    public const string ForEachRowIfColumn19Column20AndColumn21ArePresentThenAbsoluteLessThan1Percent = "Əgər sütun 19 sütun 20 və sütun 21 varsa, mütləq (fərq(sütun19*sütun20 - sütun21)/sütun21) 1 faizdən kiçik olmalıdır.";
    public const string IfColumn34IsPresentThenAtLeastOneOfColumn35OrColumn36ShouldBePresent = "Əgər sütun 34 varsa, o zaman sütun 35 və ya sütun 36-dan ən azı biri mövcud olmalıdır.";
    public const string ForEachRowIfColumn34Column35AndColumn36ArePresentThenAbsoluteLessThan1Percent = "Hər sətir üçün sütun 34 sütun 35 və sütun 36 varsa, mütləq (fərq(sütun34*sütun35 - sütun36)/sütun36) 1 faizdən kiçik olmalıdır.";
    public const string IfColumn22IsPresentThenBothColumn23AndColumn24ShouldBePresent = "Əgər sütun 22 varsa, o zaman həm sütun 23, həm də sütun 24 mövcud olmalıdır.";
    public const string IfColumn25IsPresentThenBothColumn26AndColumn27ShouldBePresent = "Əgər sütun 25 varsa, o zaman həm sütun 26, həm də sütun 27 mövcud olmalıdır.";
    public const string IfColumn28IsPresentThenBothColumn29AndColumn30ShouldBePresent = "Əgər sütun 28 varsa, o zaman həm sütun 29, həm də sütun 30 mövcud olmalıdır.";
    public const string IfColumn31IsPresentThenBothColumn32AndColumn33ShouldBePresent = "Əgər sütun 31 varsa, o zaman həm sütun 32, həm də sütun 33 mövcud olmalıdır.";
}
