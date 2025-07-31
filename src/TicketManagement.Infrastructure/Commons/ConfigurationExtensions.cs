using Microsoft.Extensions.Configuration;

namespace TicketManagement.Infrastructure.Commons;

public static partial class ConfigurationExtensions
{
    public static string? TicketDb_ConnectionString(this IConfiguration configuration)
    {
        return configuration.GetConnectionString(ConfigurationKeys.ConnectionStrings.TicketManagement);
    }

}