using BankSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankSystem.Services
{
    public interface IExchange
    {
        public decimal Converter<T, K>(decimal sum, T firstCurrencyType, K secondCurrencyType) where T : Currency where K : Currency;
    }
}
