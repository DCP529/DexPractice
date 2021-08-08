using System;
using BankSystem.Models;
using System.Collections.Generic;
using BankSystem.Exceptions;

namespace BankSystem.Services
{
    public class BankServices
    {
        private static Dictionary<Client, List<Account>> peoples = new Dictionary<Client, List<Account>>();

        private static List<Client> clients = new List<Client>();
        private static List<Employee> employees = new List<Employee>();


        public delegate decimal ExchangeHandler<T, K>(decimal sum, T firstCurrencyType, K secondCurrencyType) where T : Currency where K : Currency;

        private ExchangeHandler<Currency, Currency> _exchangeHandler;

        public void DelegateRegister(ExchangeHandler<Currency, Currency> convertHandler)
        {
            try
            {
                if (convertHandler == null)
                {
                    throw new ArgumentNullException();
                }

                _exchangeHandler = convertHandler;

            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Add<T>(T person) where T : IPerson
        {
            if (person is Client)
            {
                var client = person as Client;
                clients.Add(client);
            }
            else if (person is Employee)
            {
                var employee = person as Employee;
                employees.Add(employee);
            }
        }

        IPerson Find<T>(T person) where T : IPerson
        {
            if (person is Client)
            {
                var client = person as Client;
                return clients.Find(item => item.Passport == client.Passport);
            }
            else if (person is Employee)
            {
                var employee = person as Employee;
                return employees.Find(item => item.Passport == employee.Passport);
            }
            return null;
        }

        public IPerson FindEmployee(Employee employee)
        {
            IPerson result = Find<IPerson>(employee);
            CheckNull<IPerson>(employee);
            return result;
        }
        public IPerson FindClient(Client client)
        {
            IPerson result = Find<IPerson>(client);
            CheckNull<IPerson>(client);
            return result;
        }

        void CheckNull<T>(T result) where T : IPerson
        {
            try
            {
                if (result == null)
                {
                    throw new ArgumentNullException("Такого работника нет в списке");
                }
            }
            catch (ArgumentNullException nullException)
            {
                Console.WriteLine(nullException.Message);
            }
        }

        public void MoneyTransfer(int sum, Account donorAccaunt, Account recipientAccaunt, ExchangeHandler<Currency, Currency> convertHandler)
        {
            try
            {
                if (sum < 0)
                {
                    throw new ArgumentOutOfRangeException("Сумма не может быть отрицательной");
                }
                if (donorAccaunt.Money < convertHandler.Invoke(sum, donorAccaunt.CurrencyType, recipientAccaunt.CurrencyType))
                {
                    throw new InsufficientFundsException("Недостаточно средств на счете");
                }
                donorAccaunt.Money -= convertHandler.Invoke(sum, donorAccaunt.CurrencyType, recipientAccaunt.CurrencyType);
                recipientAccaunt.Money += sum;
            }
            catch (ArgumentOutOfRangeException argumentOutOfRangeException)
            {
                Console.WriteLine(argumentOutOfRangeException.Message);
            }
            catch (InsufficientFundsException insufficientFundsException)
            {
                Console.WriteLine(insufficientFundsException.Message);
            }
        }

        public void AddClientAccount(Client client, Account account)
        {
            List<Account> list = new List<Account>();

            if (peoples.ContainsKey(client))
            {
                peoples[client].Add(account);
            }
            else
            {
                list.Add(account);
                peoples.Add(client, list);
            }
        }
    }
}

