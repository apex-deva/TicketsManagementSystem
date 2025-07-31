using TicketManagement.Infrastructure;

namespace TicketManagement.Web;

public static class StartupDi
{
    public static void AddStartupDi(this IServiceCollection services, IConfiguration configurations)
    { 
        services.AddSharedInfra(configurations);
    }
}