using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using netChat.DTOs;

namespace netChat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly NetChatDBContext _context;

        public RoomController(NetChatDBContext context)
        {
            _context = context;
        }

        // GET all rooms
        [HttpGet]
        public async Task<IActionResult> GetRooms()
        {
            var rooms = await _context.Rooms.ToListAsync();
            return Ok(rooms);
        }

        // GET a single room by RoomId
        [HttpGet("{roomid}")]
        public async Task<IActionResult> GetRoom(string roomid)
        {
            var room = await _context.Rooms.FindAsync(roomid);
            if (room == null)
                return NotFound();
            return Ok(room);
        }



        // POST: create a room
        [HttpPost]
        public async Task<IActionResult> CreateRoom([FromBody] CreateRoomDto dto)
        {
            var room = new Room
            {
                RoomId = Guid.NewGuid().ToString().Substring(0, 6),
                RoomName = dto.RoomName,
                CreatorId = dto.CreatorId,
                UserList = ""
            };

            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                id = room.RoomId,
                name = room.RoomName,
                roomCode = room.RoomId
            });
        }



        // DELETE a room by RoomId
        [HttpDelete("{roomid}")]
        public async Task<IActionResult> DeleteRoom(string roomid)
        {
            var room = await _context.Rooms.FindAsync(roomid);
            if (room == null)
                return NotFound();

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: /Room/{roomid}/Join
        [HttpPost("{roomid}/Join")]
        public async Task<IActionResult> JoinRoom(string roomid, [FromQuery] string userId)
        {
            var room = await _context.Rooms.FindAsync(roomid);
            if (room == null)
                return NotFound("Room not found");

            var usersInRoom = room.UserList.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();

            if (!usersInRoom.Contains(userId))
                usersInRoom.Add(userId);

            room.UserList = string.Join(',', usersInRoom);

            _context.Rooms.Update(room);
            await _context.SaveChangesAsync();

            return Ok(room);
        }
        [HttpPost("{roomId}/Message")]
        public async Task<IActionResult> SendMessage(string roomId, [FromBody] Message message)
        {
            if (message == null || string.IsNullOrEmpty(message.UserId) || string.IsNullOrEmpty(message.Content))
                return BadRequest("userId and content are required.");

            message.RoomId = roomId;                // set from URL
            message.Timestamp = DateTime.UtcNow;    // current UTC timestamp

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return Ok(message);
        }


        // GET: /Room/{roomId}/Messages
        [HttpGet("{roomId}/Messages")]
        public async Task<IActionResult> GetRoomMessages(string roomId)
        {
            // Check if room exists
            var room = await _context.Rooms.FindAsync(roomId);
            if (room == null)
                return NotFound("Room not found");

            // Get all messages for the room
            var messages = await _context.Messages
                .Where(m => m.RoomId == roomId)
                .OrderBy(m => m.Timestamp) //  order by time
                .ToListAsync();

            return Ok(messages);
        }




    }
}
