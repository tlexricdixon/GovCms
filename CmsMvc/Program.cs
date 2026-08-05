
using CmsMvc.Services;
using DbContexts;
using Localization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddUserSecrets<Program>();
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<LocalDbContext>(options =>
    options.UseAzureSql(
        builder.Configuration.GetConnectionString("CmsDatabase")));
// Add localization services
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddScoped<ManagerLocalizer>();
// Register HTML sanitization service
builder.Services.AddScoped<IHtmlSanitizer, HtmlSanitizationService>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LocalDbContext>();
    //await CmsSeed.InitializeAsync(db);
}
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LocalDbContext>();
    //await CmsSeed.InitializeAsync(db);
}

await app.RunAsync();
