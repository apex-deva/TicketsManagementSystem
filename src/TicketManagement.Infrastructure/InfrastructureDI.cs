using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketManagement.Infrastructure.DIExtensions;

namespace TicketManagement.Infrastructure;

public static class InfrastructureDi
{
    public static void AddSharedInfra(this IServiceCollection services, IConfiguration configurations)
    {
        services.AddDatabases(configurations);
    }
}