using System;
using System.Collections.Generic;
using System.Text;

namespace Tamagochi.Repository.Data
{
    class User
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Login { get; set; }
        public int Level { get; set; }
        public int Money { get; set; }
    }
}
