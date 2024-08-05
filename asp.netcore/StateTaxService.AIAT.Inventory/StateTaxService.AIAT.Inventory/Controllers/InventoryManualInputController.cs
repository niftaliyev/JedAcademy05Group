using Microsoft.AspNetCore.Mvc;
using StateTaxService.AIAT.Common.Exceptions;
using StateTaxService.AIAT.Common.Models.Paginations;
using StateTaxService.AIAT.Common.Models.Responses;
using StateTaxService.AIAT.Inventory.Models;
using StateTaxService.AIAT.Inventory.Services.ExcelValidation;
using StateTaxService.AIAT.Inventory.Services.Sessions;

namespace StateTaxService.AIAT.Inventory.Controllers;

[Route("inventory")]
[ApiController]
public class InventoryManualInputController : ControllerBase
{
    private readonly IInventoryValidationService inventoryValidationService;
    private readonly ISessionPermissionService sessionPermissionService;

    public InventoryManualInputController(IInventoryValidationService inventoryValidationService, ISessionPermissionService sessionPermissionService)
    {
        this.inventoryValidationService = inventoryValidationService;
        this.sessionPermissionService = sessionPermissionService;
    }
    [HttpGet("data/{sessionId}/{fileId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ResponsePaginationModel<PagedList<UploadedExcelDataRow>>> GetUploadedExcelData(
        string sessionId,
        Guid fileId,
        [FromQuery] SearchMetaParams searchParams,
        CancellationToken cancellationToken)
    {
        var hasSessionPermission = await sessionPermissionService.IsSessionPermissionAsync(sessionId, cancellationToken);
        if (!hasSessionPermission)
        {
            throw new STSForbiddenException(string.Format(Constants.ForbiddenException, sessionId));
        }

        var result = await inventoryValidationService.GetUploadedExcelDataAsync(fileId, searchParams);
        return ResponsePaginationModel<PagedList<UploadedExcelDataRow>>.Ok(
            result,
            result.PaginationData.CurrentPage,
            result.PaginationData.TotalPages,
            result.PaginationData.PageSize,
            result.PaginationData.TotalCount);
    }
    [HttpPost("upload/{sessionId}")]
    public async Task<ResponseModel<UploadAndValidateExcelResponseModel>> UploadAndValidateExcel(
        IFormFile file,
        string sessionId,
        CancellationToken cancellationToken
        )
    {
        var hasSessionPermission = await sessionPermissionService.IsSessionPermissionAsync(sessionId, cancellationToken);
        if (!hasSessionPermission)
        {
            throw new STSForbiddenException(string.Format(Constants.ForbiddenException, sessionId));
        }

        if (file == null || file.Length == 0)
            throw new STSValidationErrorException(Constants.FileIsNotSelected);
        if (Path.GetExtension(file.FileName) != ".xlsx")
            throw new STSValidationErrorException(Constants.IncorrectFileFormat);

        var validateDataTypes = await inventoryValidationService.UploadAndValidateExcelDataAsync(file);

        return ResponseModel<UploadAndValidateExcelResponseModel>.Ok(validateDataTypes);
    }
}


