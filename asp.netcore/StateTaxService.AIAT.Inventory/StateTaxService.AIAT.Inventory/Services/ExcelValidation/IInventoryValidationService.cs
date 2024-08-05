using StateTaxService.AIAT.Common.Models.Paginations;
using StateTaxService.AIAT.Inventory.Models;

namespace StateTaxService.AIAT.Inventory.Services.ExcelValidation;

public interface IInventoryValidationService
{
    Task<UploadAndValidateExcelResponseModel> UploadAndValidateExcelDataAsync(IFormFile file);

    Task<PagedList<UploadedExcelDataRow>> GetUploadedExcelDataAsync(Guid fileId, SearchMetaParams searchParams);
}
