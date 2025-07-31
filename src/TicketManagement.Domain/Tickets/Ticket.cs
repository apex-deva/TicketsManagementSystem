using TicketManagement.Domain.Base;
using TicketManagement.Domain.Tickets.Events;
using TicketManagement.Domain.Tickets.Rules;

namespace TicketManagement.Domain.Tickets;

public class Ticket : Entity, IAggregateRoot
{
    public TicketId Id { get; private set; }
    public DateTime CreationDateTime { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Governorate { get; private set; }
    public string City { get; private set; }
    public string District { get; private set; }
    public TicketStatus Status { get; private set; }
    public DateTime? HandledDateTime { get; private set; }

    private Ticket()
    {
    } // EF Core

    private Ticket(Guid id,  string phoneNumber, string governorate, string city, string district)
    {
        Id = new TicketId(id);
        CreationDateTime = DateTime.UtcNow;
        PhoneNumber = phoneNumber;
        Governorate = governorate;
        City = city;
        District = district;
        Status = TicketStatus.Open;
        
        AddDomainEvent(new TicketCreatedDomainEvent(Id));
    }

    public void Handle()
    {
        CheckRule(new HandleTicketRule(this));
        Status = TicketStatus.Handled;
        HandledDateTime = DateTime.UtcNow;
    }

    public void AutoHandle()
    {
        CheckRule(new HandleTicketRule(this));
        CheckRule(new AutoHandleTicketRule(this));
        Status = TicketStatus.Handled;
        HandledDateTime = DateTime.UtcNow;
    }

    public TicketColorCode GetColorCode()
    {
        var minutesSinceCreation = (DateTime.UtcNow - CreationDateTime).TotalMinutes;

        return minutesSinceCreation switch
        {
            >= 60 => TicketColorCode.Red,
            >= 45 => TicketColorCode.Blue,
            >= 30 => TicketColorCode.Green,
            >= 15 => TicketColorCode.Yellow,
            _ => TicketColorCode.Default
        };
    }
    
    public static Ticket Create(Guid id, string phoneNumber, string governorate, string city, string district)
    {
        return new Ticket(id, phoneNumber, governorate, city, district);
    }
}