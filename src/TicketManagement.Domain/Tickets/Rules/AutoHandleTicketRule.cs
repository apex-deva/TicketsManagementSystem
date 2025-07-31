using TicketManagement.Domain.Base;

namespace TicketManagement.Domain.Tickets.Rules;

public class AutoHandleTicketRule : IBusinessRule
{
    private readonly Ticket _ticket;

    public AutoHandleTicketRule(Ticket ticket)
    {
        _ticket = ticket;
    }

    public bool IsBroken()
    {
        var timeSinceCreation = DateTime.UtcNow - _ticket.CreationDateTime;
        return timeSinceCreation.TotalMinutes < 60;
    }

    public string Message => "Ticket cannot be automatically handled as it is not open for more than 60 minutes";
}