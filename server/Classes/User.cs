using System;

namespace netChat
{
    public class User
    {
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

        // Empty constructor
        public User() { }

        // Constructor with username
        public User(string username)
        {
            this.username = username;
        }

        // Constructor with id + username
        public User(int user_id, string username)
        {
            this.user_id = user_id;
            this.username = username;
        }
    }
}
