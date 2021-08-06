using BankSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankSystem.Services
{
    public interface IExchange
    {
        public decimal Converter(decimal sum, Currency firstCurrencyType, Currency secondCurrencyType);
    }
}
