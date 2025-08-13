using TicketManagement.Domain.Tickets;

namespace TicketManagement.Web.Services;

public class TicketAutoHandlerService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<TicketAutoHandlerService> _logger;
    private readonly int _intervalMinutes;

    public TicketAutoHandlerService(IServiceProvider serviceProvider, ILogger<TicketAutoHandlerService> logger , IConfiguration configuration)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _intervalMinutes = configuration.GetValue<int>("BackgroundServices:TicketAutoHandler:IntervalMinutes");
    }
  
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(TimeSpan.FromMinutes(_intervalMinutes));

        while (!stoppingToken.IsCancellationRequested && 
               await timer.WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var ticketRepository = scope.ServiceProvider.GetRequiredService<ITicketRepository>();
                    
                var (tickets, _) = await ticketRepository.GetPagedAsync(1, 1000);
                    
                foreach (var ticket in tickets.Where(t => t.Status == TicketStatus.Open))
                {
                    ticket.AutoHandle();
                }
                    
                await ticketRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while auto-handling tickets");
            }

          
        }
    }
}