namespace StateTaxService.AIAT.Inventory.Services.Sessions;

public interface ISessionPermissionService
{
    Task<bool> IsSessionPermissionAsync(
        string sessionId,
        CancellationToken cancellationToken = default);
}

