using System;

namespace server.src;

class User
{
    //Variables
    private int user_id = -1;
    public int userID
    {
        get { return user_id; }
        set { user_id = value; }
    }

    private string username = "User";
    public string userName
    {
        get { return username; }
        set { username = value; }
    }

    //Constructor
    public User() {}
    
    //Constructor with username parameters
    public User(string username) 
    {
        this.username = username;
    }

    //Constructor with user_id and username parameters
    public User(int user_id, string username)
    {
        this.user_id = user_id;
        this.username = username;
    }

}