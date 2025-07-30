namespace TicketManagement.Domain.Base;

public interface IDomainEvent
{
    Guid Id { get; }

    DateTime OccurredOn { get; }
}