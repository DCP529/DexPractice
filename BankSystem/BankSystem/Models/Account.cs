using System;
using System.Collections.Generic;
using System.Text;

namespace BankSystem.Models
{
    public class Account
    {
        public decimal Money { get; set; }
        public Currency CurrencyType { get; set; }

        public Account()
        {

        }
    }
}
