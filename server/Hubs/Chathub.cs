using Microsoft.AspNetCore.SignalR;

namespace netChat.Hubs;

public class ChatHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        Console.WriteLine($"[Hub] Received from {user}: {message}");
        await Clients.All.SendAsync("ReceiveMessage", user, message);


    }
}
