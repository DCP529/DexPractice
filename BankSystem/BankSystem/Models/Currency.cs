using System;
using System.Collections.Generic;
using System.Text;

namespace BankSystem.Models
{
    public class Currency
    {
        public string Name { get; }
        public int Price { get; }
        public Currency(string name, int price)
        {
            Name = name;
            Price = price;
        }
    }
}
