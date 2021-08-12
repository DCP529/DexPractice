using BankSystem.Models;
using BankSystem.Services;
using System;
using System.IO;

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

            Client client = new Client() { Name = "Ben", Age = 10, Passport = 1 };
            Employee employee = new Employee() { Name = "Gof", Age = 15, Passport = 2 };
            var exchangeHandler = new Func<decimal, Currency, Currency, decimal>(exchange.Converter);

            bankServices.AddClientAccount(client, account2);
            bankServices.AddClientAccount(client, account1);
            bankServices.MoneyTransfer(10_000, account1, account2, exchangeHandler);


            var client1 = bankServices.FindClient(client);
            var emplowefwe = bankServices.FindEmployee(employee);

            Console.ReadLine();
        }
    }
}