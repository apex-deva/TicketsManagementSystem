using MediatR;

namespace TicketManagement.Application.Commands;

public record HandleTicketCommand(Guid TicketId) : IRequest<bool>;