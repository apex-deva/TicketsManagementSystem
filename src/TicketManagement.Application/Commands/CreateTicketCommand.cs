using MediatR;
using TicketManagement.Application.DTOs;

namespace TicketManagement.Application.Commands;

public record CreateTicketCommand(
    string PhoneNumber,
    string Governorate,
    string City,
    string District
) : IRequest<TicketDto>;