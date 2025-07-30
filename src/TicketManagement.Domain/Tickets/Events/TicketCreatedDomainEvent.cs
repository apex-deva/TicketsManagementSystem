using TicketManagement.Domain.Base;

namespace TicketManagement.Domain.Tickets.Events;

public class TicketCreatedDomainEvent : DomainEventBase
{
    public TicketCreatedDomainEvent(TicketId ticketId)
    {
        TicketId = ticketId;
    }

    public TicketId TicketId { get; }
}