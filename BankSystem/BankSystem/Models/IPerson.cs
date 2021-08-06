using System;
using System.Collections.Generic;
using System.Text;

namespace BankSystem.Models
{
    public interface IPerson
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int Passport { get; set; }

        public int GetHashCode();
        public bool Equals(object obj);
    }
}
