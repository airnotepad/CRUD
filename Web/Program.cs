using Application;
using Infrastructure;
using Serilog;

Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog();

    builder.Services.AddApplication();
    builder.Services.AddInfrastructure();
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddControllersWithViews().AddNewtonsoftJson();

    var app = builder.Build();
    Log.Information("Starting web host");

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<Infrastructure.Database.AppDbContext>();

            await Infrastructure.Database.AppDbContextSeed.SeedSampleDataAsync(context);
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "An error occurred while migrating or seeding the database.");

            return;
        }
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Dashboard}/{action=Index}/{id?}");
    });

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
    return;
}
finally
{
    Log.CloseAndFlush();
}