using Microsoft.AspNetCore.Mvc;
using server.src;

namespace server.src.controllers;

[ApiController]
[Route("[controller]")]
public class RoomController : ControllerBase
{
    private static List<Room> rooms = new List<Room>();

    [HttpPost("{username}")]
    private IActionResult CreateRoom(string username)
    {
        User creator = new User(username);
        Room newRoom = new Room();
        rooms.Add(newRoom);
        newRoom.startup(creator);
        return CreatedAtAction("placeholder", new { code = newRoom.roomCode }, newRoom);
    }

    [HttpDelete("{code}")]
    private IActionResult DeleteRoom(string code)
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
    private IActionResult GetRooms()
    {
        foreach (var room in rooms)
        {
            // Potentially update room status here if needed
        }
        return Ok(rooms);
    }

    [HttpGet("{code}")]
    private IActionResult GetRoom(string code)
    {
        Room? room = rooms.FirstOrDefault(r => r.roomCode == code);
        if (room == null)
        {
            return NotFound();
        }
        return Ok(room);
    }
}