using Microsoft.AspNetCore.Mvc;
using server.src;

namespace server.src.controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private static List<User> users = new List<User>();

    [HttpPost]
    private IActionResult AddUser(User user)
    {
        users.Add(user);
        return CreatedAtAction("placeholder", new { code = user.userName }, user);
    }

    [HttpDelete("{username}")]
    private IActionResult GetRoom(string username)
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
    private IActionResult GetUsers()
    {
        foreach (var user in users)
        {
            // Potentially update users' status here if needed
        }
        return Ok(users);
    }

    [HttpGet("{username}")]
    private IActionResult GetUser(string username)
    {
        User? user = users.FirstOrDefault(u => u.userName == username);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

}