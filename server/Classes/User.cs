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
        Password = string.Empty;
        userID = -1;
    }

    //Constructor with user_id and username parameters
    public User(string username, int user_id)
    {
        Username = username;
        Password = string.Empty;
        userID = user_id;
    }

    //Constructor with username and password parameters
    public User(string username, string password)
    {
        Username = username;
        Password = password;
        userID = -1;
    }

    //Constructor with username, password, and userID parameters
    public User(string username, string password, int user_id)
    {
        Username = username;
        Password = password;
        userID = user_id;
    }
}