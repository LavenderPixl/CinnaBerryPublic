using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinnaBerry.Models
{
    public class User
    {
        public static User Instance { get; private set; }
        public static void SetUser(User user)
        {
            Instance = user;
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<string> MoviesWatched { get; set; }
        public List<string> SeriesWatched { get; set; }
        public User(int id, string username, string email, string password)
        {
            Id = id;
            Username = username;
            Email = email;
            Password = password;
        }
    }
}
