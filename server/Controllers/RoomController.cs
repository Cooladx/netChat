using Microsoft.AspNetCore.Mvc;
using netChat.Classes;

namespace netChat.Controllers;

[ApiController]
[Route("[Controller]")]
public class RoomController : ControllerBase
{
    private static List<Room> rooms = new List<Room>();

    [HttpPost]
    public IActionResult CreateRoom([FromBody] string username)
    {
        User creator = new User(username);
        Room newRoom = new Room();
        rooms.Add(newRoom);
        newRoom.startup(creator);
        return CreatedAtAction("placeholder", new { code = newRoom.roomCode }, newRoom);
    }

    [HttpDelete]
    public IActionResult DeleteRoom(string code)
    {
        Room? room = rooms.FirstOrDefault(r => r.roomCode == code);
        if (room == null)
        {
            return NotFound();
        }
        rooms.Remove(room);
        room.shutdown();
        return NoContent();
    }

    [HttpGet]
    public IActionResult GetRooms()
    {
        foreach (var room in rooms)
        {
            // Potentially update room status here if needed
        }
        return Ok(rooms);
    }

    [HttpGet]
    public IActionResult GetRoom(string code)
    {
        Room? room = rooms.FirstOrDefault(r => r.roomCode == code);
        if (room == null)
        {
            return NotFound();
        }
        return Ok(room);
    }
}