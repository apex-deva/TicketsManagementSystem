using MediatR;
using TicketManagement.Application.DTOs;
using TicketManagement.Application.Queries;
using TicketManagement.Application.Shared;
using TicketManagement.Domain.Tickets;

namespace TicketManagement.Application.Handlers;

public class GetTicketsQueryHandler: IRequestHandler<GetTicketsQuery, PagedResult<TicketDto>>
{
    private readonly ITicketRepository _ticketRepository;

    public GetTicketsQueryHandler(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public async Task<PagedResult<TicketDto>> Handle(GetTicketsQuery request, CancellationToken cancellationToken)
    {
        // Fetch tickets from the repository with pagination
        var paginationResult = await _ticketRepository.GetPagedAsync(request.Page, request.PageSize);
        // Map the tickets to TicketDto
        var ticketDtos = paginationResult.tickets.Select(ticket => new TicketDto
        {
            Id = ticket.Id.Value,
            CreationDateTime = ticket.CreationDateTime,
            PhoneNumber = ticket.PhoneNumber,
            Governorate = ticket.Governorate,
            City = ticket.City,
            District = ticket.District,
            Status = ticket.Status.ToString(),
            ColorCode = ticket.GetColorCode().ToString()
        }).ToList();

        // Create a paged result
        var totalCount =paginationResult.totalCount;
        var pagedResult = new PagedResult<TicketDto>
        {
            Items = ticketDtos,
            TotalCount = totalCount,
            Page = request.Page,
            PageSize = request.PageSize
        };

        return pagedResult;
    }
}