using TicketManagement.Domain.Base;

namespace TicketManagement.Domain.Tickets;

public class TicketId: TypedIdValueBase
{
    public TicketId(Guid value)
        : base(value)
    {
    }
}