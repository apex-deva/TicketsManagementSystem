using Microsoft.EntityFrameworkCore;
using TicketManagement.Domain.Tickets;

namespace TicketManagement.Infrastructure.Domain.Tickets;

public class TicketRepository : ITicketRepository
{
    private readonly TicketContext _context;

    public TicketRepository(TicketContext context)
    {
        _context = context;
    }

    public async Task<Ticket?> GetByIdAsync(Guid id)
    {
        TicketId ticketId = new TicketId(id);
        return await _context.Tickets.FindAsync(ticketId);
    }

    public async Task<(List<Ticket> tickets, int totalCount)> GetPagedAsync(int page, int pageSize)
    {
        var query = _context.Tickets.OrderByDescending(t => t.CreationDateTime);
        var totalCount = await query.CountAsync();
        var tickets = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (tickets, totalCount);
    }

    public async Task AddAsync(Ticket? ticket)
    {
        await _context.Tickets.AddAsync(ticket);
    }

    public void UpdateAsync(Ticket? ticket)
    { 
        if(ticket == null) return;
        _context.Tickets.Update(ticket);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}