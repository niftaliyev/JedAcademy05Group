using Microsoft.Extensions.Options;
using StateTaxService.AIAT.Common.Exceptions;
using StateTaxService.AIAT.Common.Models.Settings;
using StateTaxService.AIAT.Inventory.Extensions;
using StateTaxService.AIAT.Inventory.Models;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;


namespace StateTaxService.AIAT.Inventory.Services.Sessions;

public class SessionPermissionService : ISessionPermissionService
{
    private readonly HttpClient httpClient;
    private readonly SessionSettings sessionSettings;
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly ILogger<SessionPermissionService> logger;

    public SessionPermissionService(
        HttpClient httpClient,
        IOptions<SessionSettings> options,
        IHttpContextAccessor httpContextAccessor,
        ILogger<SessionPermissionService> logger)
    {
        this.httpClient = httpClient;
        this.sessionSettings = options.Value;
        this.httpContextAccessor = httpContextAccessor;
        this.logger = logger;
    }

    public async Task<bool> IsSessionPermissionAsync(string sessionId, CancellationToken cancellationToken = default)
    {
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
           "Bearer",
           httpContextAccessor.HttpContext.GetJwtToken());

        var response = await httpClient.GetAsync($"{sessionSettings.SessionPermissionPath}{sessionId}", cancellationToken);
        switch (response.StatusCode)
        {
            case HttpStatusCode.OK:
                return await ParseSessionPermission(response);

            case HttpStatusCode.NotFound:
                throw new STSNotFoundException(string.Format(Constants.SessionNotFound, sessionId));

            default:
                logger.LogError($"Failed to get session permission. Status code: {response.StatusCode}");
                return false;
        }
    }

    private async Task<bool> ParseSessionPermission(HttpResponseMessage response)
    {
        using (var responseStream = await response.Content.ReadAsStreamAsync())
        {
            using (JsonDocument doc = await JsonDocument.ParseAsync(responseStream))
            {
                JsonElement root = doc.RootElement;
                if (root.TryGetProperty("data", out JsonElement dataElement) && dataElement.ValueKind == JsonValueKind.True)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
