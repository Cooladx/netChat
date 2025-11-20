using Microsoft.AspNetCore.Mvc;
using netChat.Classes;

namespace netChat.Controllers;

[ApiController]
[Route("[Controller]")]
public class UserController : ControllerBase
{
    private static List<User> users = new List<User>();

    [HttpPost]
    public IActionResult AddUser([FromBody] string username, string password)
    {
        Console.WriteLine($"Attempting to add user: {username}");
        User newUser = new User(username, password);
        users.Add(newUser);
        return CreatedAtAction("placeholder", new { code = newUser.Username }, newUser);
    }

    [HttpDelete]
    public IActionResult GetRoom(string username, string password)
    {
        User? user = users.FirstOrDefault(u => u.Username == username);
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
    public IActionResult GetUsers()
    {
        foreach (var user in users)
        {
            // Potentially update users' status here if needed
        }
        return Ok(users);
    }

    [HttpGet]
    public IActionResult GetUser(string username, string password)
    {
        User? user = users.FirstOrDefault(u => u.Username == username);
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