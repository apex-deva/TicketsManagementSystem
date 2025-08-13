using Microsoft.AspNetCore.SignalR;

namespace TicketManagement.Web.Hubs;

public class TicketHub : Hub
{
    
    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
        Console.WriteLine($"Client connected: {Context.ConnectionId}");
    }
    
    
    public async Task JoinGroup(string groupName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
    }
    
}