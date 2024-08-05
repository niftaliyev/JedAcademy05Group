namespace StateTaxService.AIAT.Inventory.Services;

public interface IFileService
{
    Task<Guid> SaveFileAsync(IFormFile file);
    Task<byte[]> GetFileAsync(Guid id);
}
