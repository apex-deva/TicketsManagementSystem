using TicketManagement.Domain.Base;

namespace TicketManagement.Domain.Tickets.Rules;

public class HandleTicketRule : IBusinessRule
{
    private readonly Ticket _ticket;

    public HandleTicketRule(Ticket ticket)
    {
        _ticket = ticket;
    }

    public bool IsBroken()
    {
        return _ticket.Status != TicketStatus.Open;
    }

    public string Message => "Cannot handle a ticket that is not open.";
}