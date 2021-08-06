using System;
using BankSystem.Models;
using System.Collections.Generic;

namespace BankSystem.Services
{
    public class BankServices
    {

        private static List<Client> clients = new List<Client>();
        private static List<Employee> employees = new List<Employee>();
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
                if (clients.Contains(client))
                {
                    return client;
                }
            }
            else if (person is Employee)
            {
                var employee = person as Employee;
                if (employees.Contains(employee))
                {
                    return employee;
                }
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
    }
}
