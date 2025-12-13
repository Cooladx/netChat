using Microsoft.AspNetCore.SignalR;

namespace netChat.Hubs;


public class ChatHub : Hub
{
    private readonly NetChatDBContext context;

    public ChatHub(NetChatDBContext context)
    {
        this.context = context;
    }

    // Join a room group
    public async Task JoinRoom(string roomId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
        Console.WriteLine($"[Hub] Connection {Context.ConnectionId} joined room {roomId}");
    }

    // Send message to a specific room
    public async Task SendMessage(string roomId, string user, string message)
    {
        Console.WriteLine($"[Hub] Received from {user} in room {roomId}: {message}");
        // Sends message to the specific group which is the room in messaging app
        await Clients.Group(roomId).SendAsync("ReceiveMessage", user, message);
    }
}