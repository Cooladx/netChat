using Microsoft.AspNetCore.Mvc;
using netChat;

namespace netChat.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private static List<User> users = new List<User>();

    [HttpPost]
    public IActionResult AddUser(User user)
    {
        users.Add(user);
        return CreatedAtAction(nameof(GetUser), new { code = user.userName }, user);
    }

    [HttpDelete("{username}")]
    public IActionResult DeleteUser(string username)
    {
        User? user = users.FirstOrDefault(u => u.userName == username);
        if (user == null)
        {
            return NotFound();
        }
        users.Remove(user);
        return NoContent();
    }

    [HttpGet]
    public IActionResult GetUsers()
    {
        foreach (var user in users)
        {
            // Potentially update users' status here if needed
        }
        return Ok(users);
    }

    [HttpGet("{username}")]
    public IActionResult GetUser(string username)
    {
        User? user = users.FirstOrDefault(u => u.userName == username);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

}
