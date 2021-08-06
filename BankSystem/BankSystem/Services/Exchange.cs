

using BankSystem.Models;

namespace BankSystem.Services
{
    public class Exchange : IExchange
    {
        public decimal Converter<T, K>(decimal sum, T firstCurrencyType, K secondCurrencyType) where T : Currency where K : Currency
        {
            return (sum / firstCurrencyType.Price) * secondCurrencyType.Price;
        }
    }
}
