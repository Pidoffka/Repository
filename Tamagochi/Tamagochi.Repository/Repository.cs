using System;
using System.Collections.Generic;
using System.Text;
using Tamagochi.Repository.Data;

namespace Tamagochi.Repository
{
    class Repository
    {
        public IEnumerable<User> Users => users;
        private List<User> users;
        public Repository()
        {

        }
        private const string userfilename = "..//";
        private void LoadData()
        {

        }
    }
}
