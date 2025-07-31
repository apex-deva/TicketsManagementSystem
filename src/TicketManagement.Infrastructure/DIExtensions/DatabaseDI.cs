using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketManagement.Infrastructure.Commons;

namespace TicketManagement.Infrastructure.DIExtensions;

public static class DatabaseDi
{
    public static void AddDatabases(this IServiceCollection services, IConfiguration configurations)
    {
        services.AddSqlDbContext<TicketContext>(configurations.TicketDb_ConnectionString());
    }

    private static IServiceCollection AddSqlDbContext<TDbContext>(this IServiceCollection services, string? connString)
        where TDbContext : DbContext
    {
        services.AddDbContext<TDbContext>(options => options.UseSqlServer(connString)
            .ConfigureWarnings(x => x.Ignore(CoreEventId.PossibleIncorrectRequiredNavigationWithQueryFilterInteractionWarning))
            .ConfigureWarnings(x => x.Ignore(RelationalEventId.BoolWithDefaultWarning)));
        return services;
    }
}