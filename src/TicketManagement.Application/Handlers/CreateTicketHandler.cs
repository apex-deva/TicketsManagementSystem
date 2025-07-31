using MediatR;
using Microsoft.Extensions.Logging;
using TicketManagement.Application.Commands;
using TicketManagement.Application.DTOs;
using TicketManagement.Domain.Tickets;

namespace TicketManagement.Application.Handlers;

public class CreateTicketHandler : IRequestHandler<CreateTicketCommand, TicketDto>
{
    private readonly ITicketRepository _ticketRepository;
    private readonly ILogger<CreateTicketHandler> _logger;

    public CreateTicketHandler(ITicketRepository ticketRepository, ILogger<CreateTicketHandler> logger)
    {
        _ticketRepository = ticketRepository;
        _logger = logger;
    }

    public async Task<TicketDto> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new ticket for phone number: {PhoneNumber}", request.PhoneNumber);

        var ticket =  Ticket.Create(Guid.NewGuid(),request.PhoneNumber, request.Governorate, request.City, request.District);
            
        await _ticketRepository.AddAsync(ticket);
        await _ticketRepository.SaveChangesAsync();

        _logger.LogInformation("Ticket created successfully with ID: {TicketId}", ticket.Id);

        return new TicketDto
        {
            Id = ticket.Id.Value,
            CreationDateTime = ticket.CreationDateTime,
            PhoneNumber = ticket.PhoneNumber,
            Governorate = ticket.Governorate,
            City = ticket.City,
            District = ticket.District,
            Status = ticket.Status.ToString(),
            ColorCode = ticket.GetColorCode().ToString()
        };
    }
}