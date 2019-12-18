using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Tamagochi.Repository.Data;

namespace Tamagochi.Repository
{
    public class Repository
    {
        public IEnumerable<User> Users => users;
        private List<User> users;
        public IEnumerable<Item> Items => items;
        private List<Item> items;
        public IEnumerable<string> TypeOfItems => typeofitems;
        private List<string> typeofitems;
        private double Duration;
        public Repository()
        {
            LoadData();
        }
        private const string usersfilename = "../../../../Tamagochi.Repository/Data/DataJson/Users.json";
        private const string itemsfilename = "../../../../Tamagochi.Repository/Data/DataJson/Items.json";
        private const string datafilename = "../../../../Tamagochi.Repository/Data/DataJson/Data.json";

        private void LoadData()
        {
            users = Deserialize<List<User>>(usersfilename);
            items = Deserialize<List<Item>>(itemsfilename);
            var data = Deserialize<Data>(datafilename);
            typeofitems = data.TypeItems;
            Duration = data.MinDuration;
        }

        public void SaveData()
        {
            Serialize(itemsfilename, items);
            Serialize(usersfilename, users);
            var data = new Data
            {
                MinDuration = Duration,
                TypeItems = typeofitems
            };
            Serialize(datafilename, data);
        }
        private class Data
        {
            public List<string> TypeItems { get; set; }
            public double MinDuration { get; set; }
        }
        private void Serialize<T>(string fileName, T data)
        {
            using (var sw = new StreamWriter(fileName))
            {
                using (var jsonWriter = new JsonTextWriter(sw))
                {
                    var serializer = new JsonSerializer();
                    serializer.Serialize(jsonWriter, data);
                }
            }
        }
        private T Deserialize<T>(string fileName)
        {
            using (var sr = new StreamReader(fileName))
            {
                using (var jsonReader = new JsonTextReader(sr))
                {
                    var serializer = new JsonSerializer();
                    return serializer.Deserialize<T>(jsonReader);
                }
            }
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
        public User checkUser(string login)
        {
            foreach (var user in users)
            {
                if (user.Login == login)
                    return user;
            }
            return null;
        }
        public void newUser(string name, string login, string password)
        {
            User user = new User(login, password, name);
            users.Add(user);
            SaveData();
        }
        public bool BuyTheItem(User user, Item item)
        {
            if (user.Money >= item.Price)
            {
                user.Money -= item.Price;
                CheckItems(user, item);
                return true;
            }
            return false;
        }
        private void AddExp(User user, int exp)
        {
            if(user.ExpLevel < user.Exp + exp)
            {
                user.Level += 1;
                user.Exp = user.Exp + exp - user.ExpLevel;
                user.ExpLevel = 30 + 20 * user.Level;
            }
            else
            {
                user.Exp = user.Exp + exp;
            }
            SaveData();
        }
        private void CheckItems(User user, Item item)
        {
            foreach (var newitem in user.Items)
            {
                if (item == newitem)
                {
                    newitem.Amount += 1;
                }
            }
            Item item1 = new Item()
            {
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                Exp = item.Exp,
                Type = item.Type,
                Amount = 1
            };
            user.Items.Add(item1);
        }
        public void UseItems(User user, Item item)
        {
            if(item.Amount == 0)
            {
                item.Amount -= 1;
                user.Items.Remove(item);
                AddExp(user, item.Exp);
            }
        }
    }
}
