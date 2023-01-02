using IdentityServer.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace IdentityServerDb;

public class SeedData
{
    public static void EnsureSeedData(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
        using (var context = scope.ServiceProvider.GetService<PersistedGrantDbContext>())
        {
            context.Database.Migrate();
        }

        using (var context = scope.ServiceProvider.GetService<ConfigurationDbContext>())
        {
            context.Database.Migrate();
        }
    }
}
