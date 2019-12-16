using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Tamagochi.Repository.Data;

namespace Tamagochi.Repository
{
    class Repository
    {
        public IEnumerable<User> Users => users;
        private List<User> users;
        public IEnumerable<Item> Items => items;
        private List<Item> items;
        public Repository()
        {
            LoadData();
        }
        private const string usersfilename = "../../../../Tamagochi.Repository/Data/DataJson/Users.json";
        private const string itemsfilename = "../../../../Tamagochi.Repository/Data/DataJson/Items.json";

        private void LoadData()
        {
            users = JsonConvert.DeserializeObject<List<User>>(usersfilename);
            items = JsonConvert.DeserializeObject<List<Item>>(itemsfilename);
        }

        public void SaveData()
        {
            Serialize(items, itemsfilename);
            Serialize(users, usersfilename);
        }
        private void Serialize<T>(T obj, string path)
        {
            string str = JsonConvert.SerializeObject(obj);
            FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate);
            StreamWriter streamWriter = new StreamWriter(fileStream);
            streamWriter.Write(str);
        }

    }
}
