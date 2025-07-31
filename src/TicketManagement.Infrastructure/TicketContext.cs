using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TicketManagement.Domain.Tickets;

namespace TicketManagement.Infrastructure;

public class TicketContext : DbContext
{
    private readonly ILoggerFactory _loggerFactory;
    public DbSet<Ticket> Tickets { get; set; }

    public TicketContext(DbContextOptions<TicketContext> options, ILoggerFactory loggerFactory) : base(options)
    {
        _loggerFactory = loggerFactory;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
         optionsBuilder.UseLoggerFactory(_loggerFactory).EnableSensitiveDataLogging();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
}