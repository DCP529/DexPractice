

using BankSystem.Models;

namespace BankSystem.Services
{
    public class Exchange : IExchange
    {
        public decimal Converter(decimal sum, Currency firstCurrencyType, Currency secondCurrencyType)
        {
            return (sum / firstCurrencyType.Price) * secondCurrencyType.Price;
        }
    }
}
