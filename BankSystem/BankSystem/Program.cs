using BankSystem.Models;
using BankSystem.Services;
using Microsoft.Data.SqlClient;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Threading.Tasks;

namespace BankSystem
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //BankServices bankServices = new BankServices();
            //Exchange exchange = new Exchange();

            //string path = Path.Combine("C:", "Users", "37377", "Documents", "GitHub", "DexPractice", "BankSystem", "DataPerson");

            //Account account1 = new Account() { Money = 15_000, CurrencyType = new Rub() };
            //Account account2 = new Account() { Money = 15_000, CurrencyType = new Ua() };

            //Client client1 = new Client() { Name = "Ben", Age = 10, Passport = 1 };
            //Client client2 = new Client() { Name = "Benis", Age = 10, Passport = 2 };

            //Employee employee = new Employee() { Name = "Gof", Age = 15, Passport = 2 };
            //var exchangeHandler = new Func<decimal, Currency, Currency, decimal>(exchange.Converter);

            //bankServices.AddClientAccount(client1, account2);
            //bankServices.AddClientAccount(client1, account1);
            ////bankServices.MoneyTransfer(10_000, account1, account2, exchangeHandler);

            //bankServices.Add<IPerson>(client2);
            //bankServices.Add<IPerson>(employee);

            //var result1 = bankServices.FindClient(client2);
            //var result2 = bankServices.FindEmployee(employee);

            //var dictionary = bankServices.CreateDictionaryFromFile(path, "Dictionary");


            //DataExport de = new DataExport();
            //de.AddDataToFile<Account>(account1, Path.Combine("C:", "Users", "37377", "Documents", "GitHub", "DexPractice", "BankSystem", "DataPerson"));

            

            Console.ReadLine();
        }
    }
}//богус генератор фкйкофых данных