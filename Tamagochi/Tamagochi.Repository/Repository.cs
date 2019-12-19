using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Tamagochi.Repository.Data;
using System.ComponentModel;

namespace Tamagochi.Repository
{
    public class Repository
    {
        public IEnumerable<User> Users => users;
        private List<User> users;
        public IEnumerable<Item> Items => items;
        private List<Item> items;
        public IEnumerable<Item> TypeOfItems => typeofitems;
        private List<Item> typeofitems;
        private double Duration;
        public Repository()
        {
            LoadData();
            FindTypeOfItems();
        }
        private const string usersfilename = "../../../../Tamagochi.Repository/Data/DataJson/Users.json";
        private const string itemsfilename = "../../../../Tamagochi.Repository/Data/DataJson/Items.json";
        private const string datafilename = "../../../../Tamagochi.Repository/Data/DataJson/Data.json";

        private void LoadData()
        {
            users = Deserialize<List<User>>(usersfilename);
            items = Deserialize<List<Item>>(itemsfilename);
            var data = Deserialize<Data>(datafilename);
            Duration = data.MinDuration;
        }

        public void SaveData()
        {
            Serialize(itemsfilename, items);
            Serialize(usersfilename, users);
            var data = new Data
            {
                MinDuration = Duration
            };
            Serialize(datafilename, data);
        }
        private class Data
        {
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
        public User checkUser<T>(string login, T password)
        {
            foreach (var user in users)
            {
                if (password == null)
                {
                    if (user.Login == login)
                        return user;
                }
                else
                {
                    if (user.Login == login & user.Password == password.ToString())
                        return user;
                }
                 
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
                if (item.ImagePath == newitem.ImagePath)
                {
                    newitem.Amount += 1;
                    return;
                }
            }
            Item item1 = new Item()
            {
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                Exp = item.Exp,
                Type = item.Type,
                Amount = 1,
                ImagePath = item.ImagePath
            };
            user.Items.Add(item1);
        }
        public void UseItems(User user, Item item)
        {
            foreach (var useritem in user.Items)
            {
                if(useritem.ImagePath == item.ImagePath)
                {
                    item.Amount -= 1;
                    AddExp(user, item.Exp);
                }
            }
            if (item.Amount == 0)
            {
                user.Items.Remove(item);
            }
        }
        public void FindTypeOfItems()
        {
            var types = new List<Item>();
            foreach (var item in items)
            {
                if(types.Where(x => x.Type == item.Type).Count() == 0)
                {
                    types.Add(item);
                }
            }
            typeofitems = types;
        }
    }
}
