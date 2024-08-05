using StateTaxService.AIAT.Common.Models;
using StateTaxService.AIAT.Common.Models.Settings;
using StateTaxService.AIAT.Inventory.Configurations;
using StateTaxService.AIAT.Inventory.Middlewares;
using StateTaxService.AIAT.Inventory.Services;
using StateTaxService.AIAT.Inventory.Services.ExcelValidation;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
    options.JsonSerializerOptions.Converters.Add(new StringTrimmerJsonConverter());
});
builder.Services.AddTransient<IFileService, FileService>();
builder.Services.AddTransient<IValidateService, ValidateService>();
builder.Services.AddTransient<IInventoryValidationService, InventoryValidationService>();
builder.Services.AddSingleton<IHardSoftCheckService, HardSoftCheckService>();
builder.Services.AddSingleton<IDataTypesCheckService, DataTypesCheckService>();
builder.Services.AddServices(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwagger();
builder.Services.AddOptions(builder.Configuration);

builder.Services.AddOptions<RedisSettings>().Bind(builder.Configuration.GetSection("RedisSettings"));


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseCors(builder => builder.AllowAnyOrigin()
                                  .AllowAnyHeader()
                                  .AllowAnyMethod());
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StateTaxService.AIAT.Inventory v1"));
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();
app.UseMiddleware<ExceptionMiddleware>();
app.Run();
