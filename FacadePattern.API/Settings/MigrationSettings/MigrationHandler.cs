using FacadePattern.API.Data.DatabaseContexts;
using Microsoft.EntityFrameworkCore;

namespace FacadePattern.API.Settings.MigrationSettings;

public static class MigrationHandler
{
    public static void MigrateDatabase(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var appContext = scope.ServiceProvider.GetRequiredService<FacadePatternDbContext>();

        try
        {
            appContext.Database.Migrate();
        }
        catch
        {
            throw;
        }
    }
}
