using StateTaxService.AIAT.Common.Models.Paginations;
using StateTaxService.AIAT.Inventory.Extensions;
using StateTaxService.AIAT.Inventory.Models;

namespace StateTaxService.AIAT.Inventory.Services.ExcelValidation;

public class InventoryValidationService : IInventoryValidationService
{
    public delegate InventoryRowValidationResult DataTypesCheckDelegate(RowData rowData);

    private readonly IFileService fileService;
    private readonly IValidateService validateService;

    public InventoryValidationService(IFileService fileService,IValidateService validateService)
    {
        this.fileService = fileService;
        this.validateService = validateService;
    }
    public async Task<UploadAndValidateExcelResponseModel> UploadAndValidateExcelDataAsync(IFormFile file)
    {
        var fileId = await fileService.SaveFileAsync(file);
        var fileResult = await fileService.GetFileAsync(fileId);
        UploadAndValidateExcelResponseModel responseModel = new UploadAndValidateExcelResponseModel();
        responseModel.FileId = fileId;
        responseModel.Result = validateService.ValidateDataTypes(fileResult);
        if (responseModel.Result.Any())
            return responseModel;

        responseModel.Result = validateService.ValidateHardAndSoftErrors(fileResult);
        return responseModel;
    }
    public async Task<PagedList<UploadedExcelDataRow>?> GetUploadedExcelDataAsync(
        Guid fileId, 
        SearchMetaParams searchParams)
    {
        var fileResult = await fileService.GetFileAsync(fileId);
        
        var rowList = validateService.GetData(fileResult);

        return new PagedList<UploadedExcelDataRow>
        (
            rowList.Pagination(searchParams.PageNumber, searchParams.PageSize),
            rowList.Count,
            searchParams.PageNumber,
            searchParams.PageSize
        );
    }
}
