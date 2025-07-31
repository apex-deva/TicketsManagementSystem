namespace TicketManagement.Domain.Tickets;

public interface ITicketRepository
{
    Task<Ticket?> GetByIdAsync(Guid id);
    Task<(List<Ticket> tickets, int totalCount)> GetPagedAsync(int page, int pageSize);
    Task AddAsync(Ticket? ticket);
    void UpdateAsync(Ticket? ticket);
    Task SaveChangesAsync();
}