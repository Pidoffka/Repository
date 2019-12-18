using System;
using System.Collections.Generic;
using System.Text;

namespace Tamagochi.Repository.Data
{
    public class User
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Login { get; set; }
        public int Level { get; set; }
        public int Money { get; set; }
        public DateTime? LastFeedingAt { get; set; }
        public int Exp { get; set; }
        public int ExpLevel { get; set; }
        public List<Item> Items { get; set; }
        public User(string login, string password, string name)
        {
            Exp = 0;
            ExpLevel = 30 + 20 * Level;
            Money = 0;
            LastFeedingAt = DateTime.Now;
            Login = login;
            Password = password;
            Name = name;
            Items = new List<Item>();
        }
    }
}
