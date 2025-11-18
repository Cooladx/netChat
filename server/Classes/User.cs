using System;

namespace netChat.Classes;

public class User
{
    //Variables
    public int userID { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    //Constructor
    public User() { }

    //Constructor with username parameters
    public User(string username)
    {
        Username = username;
        userID = -1;
    }

    //Constructor with user_id and username parameters
    public User(string username, int user_id)
    {
        userID = user_id;
        Username = username;
    }
}