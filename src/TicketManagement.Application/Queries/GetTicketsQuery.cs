using MediatR;
using TicketManagement.Application.DTOs;
using TicketManagement.Application.Shared;

namespace TicketManagement.Application.Queries;

public record GetTicketsQuery(int Page = 1, int PageSize = 5) : IRequest<PagedResult<TicketDto>>;