using Microsoft.AspNetCore.SignalR;

namespace SignalRWebpack.Hubs;

public class ChatHub : Hub
{
   public async Task SendMessage(string user, string message)
{
    try
    {
        Console.WriteLine($"Sending message from {user}: {message}");
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"SendMessage error: {ex.Message}");
        throw;
    }
}

}
