using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TicketManagement.Application.Commands;
using TicketManagement.Application.DTOs;
using TicketManagement.Application.Queries;
using TicketManagement.Application.Shared;
using TicketManagement.Web.Hubs;

namespace TicketManagement.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHubContext<TicketHub> _hubContext;

    public TicketsController(IMediator mediator, IHubContext<TicketHub> hubContext)
    {
        _mediator = mediator;
        _hubContext = hubContext;
    }
    
    [HttpPost]
    public async Task<ActionResult<TicketDto>> CreateTicket([FromBody] CreateTicketCommand command)
    {
        var ticket = await _mediator.Send(command);
        await _hubContext.Clients.All.SendAsync("TicketCreated", ticket);
        return Ok(ticket);
    }
    
    [HttpGet]
    public async Task<ActionResult<PagedResult<TicketDto>>> GetTickets([FromQuery] int page = 1, [FromQuery] int pageSize = 5)
    {
        var query = new GetTicketsQuery(page, pageSize);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpPut("{id}/handle")]
    public async Task<ActionResult> HandleTicket(Guid id)
    {
        var command = new HandleTicketCommand(id);
        var result = await _mediator.Send(command);
        if (!result) return NotFound();
        await _hubContext.Clients.All.SendAsync("TicketHandled", id);
        return Ok();

    }
}