using System;
using System.Collections.Generic;
using System.Text;

namespace Tamagochi.Repository.Data
{
    public class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Type { get; set; }
        public int Exp { get; set; }
        // В магазине всегда доступно неограниченное количество вещей,
        // Но это свойство нужно для количества предметов у пользователя
        public int Amount { get; set; }
    }
}
