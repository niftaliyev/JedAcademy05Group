using Microsoft.Extensions.Options;
using StackExchange.Redis;
using StateTaxService.AIAT.Common.Models.Settings;
using StateTaxService.AIAT.Inventory.Extensions;

namespace StateTaxService.AIAT.Inventory.Services;

public class FileService : IFileService
{
    private readonly ConnectionMultiplexer redisConnection;
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly string excelExtension = ".xlsx";
    public FileService(IOptions<RedisSettings> options,
        IHttpContextAccessor httpContextAccessor
        )
    {
        this.httpContextAccessor = httpContextAccessor;
        var redisConnectionString = options.Value.ConnectionString;
        var redisConfiguration = ConfigurationOptions.Parse(redisConnectionString);
        redisConnection = ConnectionMultiplexer.Connect(redisConfiguration);

    }
    public async Task<Guid> SaveFileAsync(IFormFile file)
    {
        var userId = httpContextAccessor.HttpContext.GetUserIdFromToken();
        using (var ms = new MemoryStream())
        {
            var database = redisConnection.GetDatabase();

            await file.CopyToAsync(ms);
            var fileData = ms.ToArray();
            var fileId = Guid.NewGuid();
            var redisKey = GetUniqueKeyForFile(fileId, userId);
            await database.StringSetAsync(redisKey, fileData, TimeSpan.FromHours(24));

            return fileId;
        }
    }
    public async Task<byte[]?> GetFileAsync(Guid fileId)
    {
        var userId = httpContextAccessor.HttpContext.GetUserIdFromToken();

        var redisKey = GetUniqueKeyForFile(fileId, userId);
        var database = redisConnection.GetDatabase();
        var fileData = await database.StringGetAsync(redisKey);
        return fileData.IsNullOrEmpty ? null : (byte[])fileData;
    }
    private string GetUniqueKeyForFile(Guid fileId, Guid userId)
    {
        return $"excel:{fileId}:{userId}.{excelExtension}";
    }
}
