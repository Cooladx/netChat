using System.Collections.Generic;
using System.Security.Cryptography;

class Room
{
    //Variables
    private int max_users = 2;
    public int maxUsers
    {
        get { return max_users; }
        set { max_users = value; }
    }

    private int cur_users = 0;
    public int curUsers
    {
        get { return cur_users; }
        set { cur_users = value; }
    }

    private string room_code = "";
    public string roomCode
    {
        get { return roomCode; }
        set { roomCode = value; }
    }

    Queue<(User, string)> messages = new Queue<(User, string)>();
    List<User> users = new List<User>();

    //Constructor
    public Room()
    {
        //Generate room code
        generateRoomCode();

        //ask for room creator to input username at this point
        
        User creator = new User( /*use input username as argument*/ );
        startup(creator);
    }

    //Called when room is created
    void startup(User user)
    {
        addUser(user);
    }

    //Called when room is unneeded (when 0 users remain?)
    void shutdown()
    {

    }

    //Called when a user connects to the room
    void addUser(User user)
    {
        ++cur_users;
        user.userID = cur_users;
        users.Add(user);
    }

    //Called when a user leaves the room
    void removeUser(User user)
    {
        users.Remove(user);
        --cur_users;
    }

    //Called when a user inputs text into the message box
    public void addMessage(User user, string message)
    {
        messages.Enqueue((user, message));
        displayMessage();
    }

    //Called after addMessage()
    void displayMessage()
    {
        //Need to ensure that multiple messages being sent at once won't overshadow each other
        //Primitive implementation
        Console.WriteLine(messages.Dequeue());
    }

    private void generateRoomCode()
    {
        //Generate a random 6 letter code
        string random = RandomNumberGenerator.GetString(
            choices: "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789",
            length: 8
        );

        //Check if it already exists
        if ( false /*Check if room code already exists in list of room codes*/)
        {
            generateRoomCode();
        }
        else
        {
            room_code = random;
        }
    }
}