using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLib
{
    public class ManagerUsers
    {
        //instance fields
        private List<User> _users;
        private Dictionary<string, User> _dicUsername;

        //ctor
        public ManagerUsers()
        {
            _users = new List<User>();
            _dicUsername = new Dictionary<string, User>();
        }

        //add user
        //return true if the username is not exist, else return false.
        public bool AddUser(User user)
        {
            if (_dicUsername.ContainsKey(user.UserName))
            {
                return false;
            }
            else
            {
                _users.Add(user);
                _dicUsername.Add(user.UserName, user);
                return true;
            }
        }

        //remove user
        //returm true if user is exist in list, else return false 
        public bool RemoveUser(string username)
        {
            if (_dicUsername.ContainsKey(username))
            {
                _users.Remove(this[username]);
                _dicUsername.Remove(username);
                return true;
            }
            return false;
        }
        public User this[string username]
        {
            get
            {
                if (_dicUsername.ContainsKey(username))
                {
                    return _dicUsername[username];
                }
                return null;

            }

        }
        //return user by parameters of username and password
        //if user is not exist in dicUsername return null (password or username are not exist)
        public User Login(string username, string password)
        {
            if (_dicUsername.ContainsKey(username)&&_dicUsername[username].Password==password)
            {
                return _dicUsername[username];
            }
            return null;//if passworsd or username are not exist
        }
        //indexer of login
        public User this[string username, string password]
        {
            get
            {
                if (_dicUsername.ContainsKey(username) && _dicUsername[username].Password == password)
                {
                    return _dicUsername[username];
                }
                return null;
            }
        }

    }
}
