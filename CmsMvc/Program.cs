using CmsMvc.Data;
using DbContexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddUserSecrets<Program>();
builder.Services.AddControllersWithViews();
//var databaseProvider =
//    builder.Configuration["DatabaseProvider"] ?? "Sqlite";
//var databaseProvider =
//    builder.Configuration["DatabaseProvider"]
//    ?? throw new InvalidOperationException(
//        "DatabaseProvider is missing.");

//var connectionString =
//    builder.Configuration.GetConnectionString("CmsDatabase")
//    ?? throw new InvalidOperationException(
//        "The CmsDatabase connection string is missing.");

//builder.Services.AddDbContext<LocalDbContext>(options =>
//{
//    if (databaseProvider.Equals(
//            "SqlServer",
//            StringComparison.OrdinalIgnoreCase))
//    {
//        options.UseSqlServer(
//            connectionString,
//            sqlOptions =>
//            {
//                sqlOptions.EnableRetryOnFailure();
//            });
//    }
//    else
//    {
//        options.UseSqlite(connectionString);
//    }
//});
builder.Services.AddDbContext<LocalDbContext>(options =>
    options.UseAzureSql(
        builder.Configuration.GetConnectionString("CmsDatabase")));

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LocalDbContext>();
    await CmsSeed.InitializeAsync(db);
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
    await CmsSeed.InitializeAsync(db);
}

await app.RunAsync();
