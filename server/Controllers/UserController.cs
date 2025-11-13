using Microsoft.AspNetCore.Mvc;
using netChat.Classes;

namespace netChat.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private static List<User> users = new List<User>();

    [HttpPost]
    private IActionResult AddUser(string username, string password)
    {
        User newUser = new User(username);
        newUser.Password = password;
        users.Add(newUser);
        return CreatedAtAction("placeholder", new { code = newUser.userName }, newUser);
    }

    [HttpDelete("{username}")]
    private IActionResult GetRoom(string username, string password)
    {
        User? user = users.FirstOrDefault(u => u.userName == username);
        if (user == null)
        {
            return NotFound();
        }
        if (user.Password != password)
        {
            return Unauthorized();
        }
        users.Remove(user);
        return NoContent();
    }

    [HttpGet]
    private IActionResult GetUsers()
    {
        foreach (var user in users)
        {
            // Potentially update users' status here if needed
        }
        return Ok(users);
    }

    [HttpGet("{username}")]
    private IActionResult GetUser(string username, string password)
    {
        User? user = users.FirstOrDefault(u => u.userName == username);
        if (user == null)
        {
            return NotFound();
        }
        if (user.Password != password)
        {
            return Unauthorized();
        }
        return Ok(user);
    }

}