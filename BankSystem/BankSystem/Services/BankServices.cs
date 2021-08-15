using System;
using BankSystem.Models;
using System.Collections.Generic;
using BankSystem.Exceptions;
using System.IO;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace BankSystem.Services
{
    public class BankServices
    {
        private static Dictionary<string, List<Account>> peoples = new Dictionary<string, List<Account>>();

        private static List<Client> clients = new List<Client>();
        private static List<Employee> employees = new List<Employee>();


        private Func<decimal, Currency, Currency, decimal> _convertHandler;

        public void DelegateRegister(Func<decimal, Currency, Currency, decimal> convertHandler)
        {
            try
            {
                if (convertHandler == null)
                {
                    throw new ArgumentNullException();
                }

                _convertHandler = convertHandler;

            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Add<T>(T person) where T : IPerson
        {
            string path = Path.Combine("C:", "Users", "37377", "Documents", "GitHub", "DexPractice", "BankSystem", "DataPerson");
            if (person is Client)
            {
                var client = person as Client;
                clients.Add(client);
                WriteFile(path, person, "Client");
            }
            else if (person is Employee)
            {
                var employee = person as Employee;
                employees.Add(employee);
                WriteFile(path, person, "Employee");
            }
        }

        private void WriteFile(string path, IPerson person, string nameFile)
        {
            using (FileStream fileStream = new FileStream($"{path}\\{nameFile}.txt", FileMode.Append))
            {
                var result = JsonConvert.SerializeObject(person);
                byte[] array = Encoding.Default.GetBytes(result);
                fileStream.Write(array, 0, array.Length);
            }
        }

        private IPerson Find<T>(T person) where T : IPerson
        {
            string path = Path.Combine("C:", "Users", "37377", "Documents", "GitHub", "DexPractice", "BankSystem", "DataPerson");

            if (person is Client)
            {
                var client = person as Client;
                return FindInFile(path, "Client", client);
            }
            else if (person is Employee)
            {
                var employee = person as Employee;
                return FindInFile(path, "Employee", employee);
            }
            return null;
        }

        private IPerson FindInFile<T>(string path, string name, T person) where T : IPerson
        {

            using (StreamReader sr = new StreamReader($"{path}\\{name}.txt"))
            {
                var result = sr.ReadToEnd();
                var desFile = JsonConvert.DeserializeObject<T>(result);

                if (desFile.Passport == person.Passport)
                {
                    return desFile;
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
        private void CheckNull<T>(T result) where T : IPerson
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

        public void MoneyTransfer(int sum, Account donorAccaunt, Account recipientAccaunt, Func<decimal, Currency, Currency, decimal> convertHandler)
        {
            try
            {
                if (sum < 0)
                {
                    throw new ArgumentOutOfRangeException("Сумма не может быть отрицательной");
                }
                if (donorAccaunt.Money < sum)
                {
                    throw new InsufficientFundsException("Недостаточно средств на счете");
                }

                donorAccaunt.Money -= sum;
                recipientAccaunt.Money += convertHandler.Invoke(sum, donorAccaunt.CurrencyType, recipientAccaunt.CurrencyType);
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

            if (peoples.ContainsKey(client.Passport.ToString()))
            {
                peoples[client.Passport.ToString()].Add(account);
            }
            else
            {
                list.Add(account);
                peoples.Add(client.Passport.ToString(), list);
            }

            AddDictionaryInFile();
        }

        private void AddDictionaryInFile()
        {
            string path = Path.Combine("C:", "Users", "37377", "Documents", "GitHub", "DexPractice", "BankSystem", "DataPerson");

            using (StreamWriter sw = new StreamWriter($"{path}\\Dictionary.txt"))
            {
                string result = JsonConvert.SerializeObject(peoples);
                sw.Write(result);
            }
        }

        public Dictionary<string, List<Account>> CreateDictionaryFromFile(string path, string fileName)
        {
            using (StreamReader sr = new StreamReader($"{path}\\{fileName}.txt"))
            {
                var result = sr.ReadToEnd();
                var desFile = JsonConvert.DeserializeObject<Dictionary<string, List<Account>>>(result);

                return desFile;
            }
        }

    }
}
