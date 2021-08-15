﻿using BankSystem.Models;
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

            Client client1 = new Client() { Name = "Ben", Age = 10, Passport = 1 };
            Client client2 = new Client() { Name = "Benis", Age = 10, Passport = 2 };

            Employee employee = new Employee() { Name = "Gof", Age = 15, Passport = 2 };
            var exchangeHandler = new Func<decimal, Currency, Currency, decimal>(exchange.Converter);

            bankServices.AddClientAccount(client1, account2);
            bankServices.AddClientAccount(client1, account1);
            bankServices.MoneyTransfer(10_000, account1, account2, exchangeHandler);

            string path = Path.Combine("C:", "Users", "37377", "Documents", "GitHub", "DexPractice", "BankSystem", "DataPerson");

            var client = bankServices.FindClient(client1);

            client = bankServices.FindClient(client2);

            Console.ReadLine();
        }
    }
}