using Microsoft.AspNetCore.Mvc;
using netChat;
using System.Collections.Generic;
using System.Linq;

namespace netChat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : ControllerBase
    {
        private static List<Room> rooms = new List<Room>();

        [HttpPost("{username}")]
        public IActionResult CreateRoom(string username)
        {
            User creator = new User(username);
            Room newRoom = new Room(creator);  
            rooms.Add(newRoom);

            return CreatedAtAction(nameof(GetRoom), new { code = newRoom.roomCode }, newRoom);
        }

        [HttpDelete("{code}")]
        public IActionResult DeleteRoom(string code)
        {
            Room? room = rooms.FirstOrDefault(r => r.roomCode == code);
            if (room == null)
                return NotFound();

            rooms.Remove(room);
            room.Shutdown();

            return NoContent();
        }

        [HttpGet]
        public IActionResult GetRooms()
        {
            return Ok(rooms);
        }

        [HttpGet("{code}")]
        public IActionResult GetRoom(string code)
        {
            Room? room = rooms.FirstOrDefault(r => r.roomCode == code);
            if (room == null)
                return NotFound();

            return Ok(room);
        }
    }
}
