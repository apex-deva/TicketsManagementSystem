using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketManagement.Domain.Tickets;
using TicketManagement.Infrastructure.DIExtensions;
using TicketManagement.Infrastructure.Domain.Tickets;

namespace TicketManagement.Infrastructure;

public static class InfrastructureDi
{
    public static void AddSharedInfra(this IServiceCollection services, IConfiguration configurations)
    {
        services.AddDatabases(configurations);
        services.AddScoped<ITicketRepository, TicketRepository>();
    }
}