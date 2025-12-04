using Microsoft.AspNetCore.Mvc;
using netChat.Classes;

namespace netChat.Controllers;

[ApiController]
[Route("[Controller]")]
public class UserController(NetChatDBContext context) : ControllerBase
{
    private readonly NetChatDBContext context = context;

    [HttpPost]
    public IActionResult AddUser([FromBody] string username, string password)
    {
        Console.WriteLine($"Attempting to add user: {username}");
        User newUser = new(username, password);
        context.Users.Add(newUser);
        return CreatedAtAction("placeholder", new { code = newUser.Username }, newUser);
    }

    [HttpDelete]
    public IActionResult GetRoom(string username, string password)
    {
        User? user = context.Users.FirstOrDefault(u => u.Username == username);
        if (user == null)
        {
            return NotFound();
        }
        if (user.Password != password)
        {
            return Unauthorized();
        }
        context.Users.Remove(user);
        return NoContent();
    }

    [HttpGet]
    public IActionResult GetUsers()
    {
        foreach (var user in context.Users)
        {
            // Potentially update users' status here if needed
        }
        return Ok(context.Users);
    }

    [HttpGet]
    public IActionResult GetUser(string username, string password)
    {
        User? user = context.Users.Where(u => u.Username == username).First();
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