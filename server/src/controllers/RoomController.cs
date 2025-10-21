using server.src;

namespace server.src.controllers;

[ApiController]
[Route("[controller]")]
public class RoomController : ControllerBase
{
    private static List<Room> rooms = new List<Room>();

    [HttpPost]
    public IActionResult CreateRoom(User creator)
    {
        Room newRoom = new Room();
        rooms.Add(newRoom);
        newRoom.startup(creator);
        return CreatedAtAction(nameof(Get), new { code = newRoom.roomCode }, newRoom);
    }

    [HttpDelete("{code}")]
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
}