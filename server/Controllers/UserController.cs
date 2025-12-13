using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using netChat;

namespace netChat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly NetChatDBContext _context;

        public UserController(NetChatDBContext context)
        {
            _context = context;
        }

        // GET all users
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        // GET a single user by userid
        [HttpGet("{userid}")]
        public async Task<IActionResult> GetUser(string userid)
        {
            var user = await _context.Users.FindAsync(userid);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        // POST: create a user
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            // Assign unique ID if not provided
            if (string.IsNullOrEmpty(user.UserId))
                user.UserId = Guid.NewGuid().ToString();

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { userid = user.UserId }, user);
        }

        // DELETE a user by userid
        [HttpDelete("{userid}")]
        public async Task<IActionResult> DeleteUser(string userid)
        {
            var user = await _context.Users.FindAsync(userid);
            if (user == null)
                return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Sign up endpoint
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] User newUser)
        {
            // Optional: check if username already exists
            if (await _context.Users.AnyAsync(u => u.UserName == newUser.UserName))
                return BadRequest(new { message = "Username already exists" });

            // Generate unique userId
            newUser.UserId = Guid.NewGuid().ToString().Substring(0, 6);

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(new { userId = newUser.UserId, username = newUser.UserName });
        }

        // Login in endpoint for the login page
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] User loginUser)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == loginUser.UserName
                                       && u.Password == loginUser.Password);

            if (user == null)
                return Unauthorized(new { message = "Invalid username or password" });
            Console.WriteLine($"Login attempt: {loginUser.UserName} / {loginUser.Password}");

            return Ok(new { userId = user.UserId, username = user.UserName });
        }

    }

}
