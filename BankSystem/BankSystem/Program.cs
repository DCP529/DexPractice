using BankSystem.Models;
using BankSystem.Services;
using System;

namespace BankSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            BankServices bankServices = new BankServices();
            Exchange exchange = new Exchange();

            Account account1 = new Account() { Money = 15_000, CurrencyType = new Rub() };
            Account account2 = new Account() { Money = 15_000, CurrencyType = new Ua() };

            Client client = new Client() { Name = "", Age = 150, Passport = 1 };

            var exchangeHandler = new BankServices.ExchangeHandler<Currency, Currency>(exchange.Converter);

            bankServices.AddClientAccount(client, account2);
            bankServices.AddClientAccount(client, account1);
            bankServices.MoneyTransfer(10_000, account1, account2, exchangeHandler);

            Console.ReadLine();
        }
    }
}
