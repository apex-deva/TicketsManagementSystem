namespace TicketManagement.Application.DTOs;

public class TicketDto
{
    public Guid Id { get; set; }
    public DateTime CreationDateTime { get; set; }
    public string PhoneNumber { get; set; }
    public string Governorate { get; set; }
    public string City { get; set; }
    public string District { get; set; }
    public string Status { get; set; }
    public string ColorCode { get; set; }
}