using Microsoft.Extensions.Options;
using MovieApi.Extensions;
using MovieApi.Options;
using MovieApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<ISearch,MovieApiService>();
builder.Services.AddHttpClient();

builder.Services.AddMovieApi(options =>
{
    options.ApiKey = builder.Configuration["MovieApi:ApiKey"];
    options.BaseUrl = builder.Configuration["MovieApi:BaseUrl"];
});

//builder.Services.Configure<MovieApiOptions>(options =>
//{
//    options.ApiKey = builder.Configuration["MovieApi:ApiKey"];
//    options.BaseUrl = builder.Configuration["MovieApi:BaseUrl"];
//});


//builder.Services.AddSingleton
//builder.Services.AddTransient    = new MyService();
//builder.Services.AddScoped

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapDefaultControllerRoute();

app.Run();
