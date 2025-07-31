using MediatR;
using TicketManagement.Application.Commands;
using TicketManagement.Domain.Tickets;

namespace TicketManagement.Application.Handlers;

public class HandleTicketCommandHandler : IRequestHandler<HandleTicketCommand, bool>
{
    private readonly ITicketRepository _ticketRepository;

    public HandleTicketCommandHandler(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public async Task<bool> Handle(HandleTicketCommand request, CancellationToken cancellationToken)
    {
        // Fetch the ticket from the repository
        var ticket = await _ticketRepository.GetByIdAsync(request.TicketId);
        if (ticket == null)
        {
            return false;
        }
        
        ticket.Handle();
         _ticketRepository.UpdateAsync(ticket);
        await _ticketRepository.SaveChangesAsync();
    return true;
    }
}