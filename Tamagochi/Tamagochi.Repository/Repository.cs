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
        private double Duration;
        public Repository()
        {
            LoadData();
        }
        private const string usersfilename = "../../../../Tamagochi.Repository/Data/DataJson/Users.json";
        private const string itemsfilename = "../../../../Tamagochi.Repository/Data/DataJson/Items.json";
        private const string durationfilename = "../../../../Tamagochi.Repository/Data/DataJson/Duration.json";

        private void LoadData()
        {
            users = JsonConvert.DeserializeObject<List<User>>(usersfilename);
            items = JsonConvert.DeserializeObject<List<Item>>(itemsfilename);
            Duration = JsonConvert.DeserializeObject<double>(durationfilename);
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
        public bool CheckTime(User user)
        {
            TimeSpan interval = DateTime.Now - user.LastFeedingAt;
            if(interval.TotalMinutes <= Duration)
            {
                return true;
            }
            return false;
        }
        public List<Item> ShowItem(string type)
        {
            List<Item> _items = new List<Item>();
            foreach (var item in items)
            {
                if(item.Type == type)
                {
                    _items.Add(item);
                }
            }
            return _items;
        }
    }
}
