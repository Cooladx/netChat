using Microsoft.AspNetCore.Mvc;
using netChat.Classes;

namespace netChat.Controllers;

[ApiController]
[Route("[Controller]")]
public class RoomController(NetChatDBContext context) : ControllerBase
{
    private readonly NetChatDBContext context = context;

    [HttpPost]
    public IActionResult CreateRoom([FromBody] string username)
    {
        User creator = context.Users.Where(u => u.Username == username).First();
        Room newRoom = new();
        context.Rooms.Add(newRoom);
        newRoom.startup(creator);
        return CreatedAtAction("placeholder", new { code = newRoom.roomCode }, newRoom);
    }

    [HttpDelete]
    public IActionResult DeleteRoom(string code)
    {
        Room? room = context.Rooms.Where(r => r.roomCode == code).First();
        if (room == null)
        {
            return NotFound();
        }
        context.Rooms.Remove(room);
        room.shutdown();
        return NoContent();
    }

    [HttpGet]
    public IActionResult GetRooms()
    {
        foreach (var room in context.Rooms)
        {
            // Potentially update room status here if needed
        }
        return Ok(context.Rooms);
    }

    [HttpGet]
    public IActionResult GetRoom(string code)
    {
        Room? room = context.Rooms.Where(r => r.roomCode == code).First();
        if (room == null)
        {
            return NotFound();
        }
        return Ok(room);
    }
}