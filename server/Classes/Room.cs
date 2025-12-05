using System;
using System.Collections.Generic;

namespace netChat
{
    public class Room
    {
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

        private string room_code;
        public string roomCode
        {
            get { return room_code; }
            set { room_code = value; }
        }

        private Queue<(User, string)> messages = new Queue<(User, string)>();
        private List<User> users = new List<User>();

        // Constructor that takes a creator
        public Room(User creator)
        {
            roomCode = Guid.NewGuid().ToString().Substring(0, 6); // assign unique code
            Startup(creator);
        }

        public void Startup(User user)
        {
            AddUser(user);
        }

        public void Shutdown()
        {
            users.Clear();
            messages.Clear();
            cur_users = 0;
        }

        public void AddUser(User user)
        {
            cur_users++;
            user.userID = cur_users;
            users.Add(user);
        }

        public void RemoveUser(User user)
        {
            users.Remove(user);
            cur_users--;
        }

        public void AddMessage(User user, string message)
        {
            messages.Enqueue((user, message));
            DisplayMessage();
        }

        private void DisplayMessage()
        {
            Console.WriteLine(messages.Dequeue());
        }
    }
}
