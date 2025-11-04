using Microsoft.AspNetCore.SignalR;

namespace netChat.Hubs;

public class ChatHub(NetChatDBContext context) : Hub
{
    private readonly NetChatDBContext context = context;

    public async Task SendMessage(string user, string message)
    {
        // TODO: Access DB
        Console.WriteLine($"[Hub] Received from {user}: {message}");
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}
