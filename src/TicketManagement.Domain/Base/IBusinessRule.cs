namespace TicketManagement.Domain.Base;

public interface IBusinessRule
{
    bool IsBroken();

    string Message { get; }
}