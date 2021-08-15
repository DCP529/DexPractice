using System;
using BankSystem.Models;
using System.Collections.Generic;
using BankSystem.Exceptions;
using System.IO;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace BankSystem.Services
{
    public class BankServices
    {
        private static Dictionary<Client, List<Account>> peoples = new Dictionary<Client, List<Account>>();

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
                byte[] array = Encoding.Default.GetBytes($"Имя - {person.Name}, Возраст - {person.Age} лет, Паспорт - {person.Passport} \n");
                fileStream.Write(array, 0, array.Length);
            }
        }

        private IPerson Find<T>(T person) where T : IPerson
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

        //private IPerson FindInFile(string path, string name, IPerson person) // не работает
        //{
        //    using (FileStream fileStream = new FileStream($"{path}\\{name}.txt", FileMode.Open))
        //    {
        //        byte[] array = new byte[fileStream.Length];
        //        fileStream.Read(array, 0, array.Length);
        //        string textFromFile = Encoding.Default.GetString(array);

        //        Match match = Regex.Match(textFromFile, "Ben - 10 лет, паспорт - (.*?)");
        //        if (match.Groups[1].Value.Contains(person.Passport.ToString()))
        //        {
        //            return person;
        //        }
        //    }
        //    return null;
        //}

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

            if (peoples.ContainsKey(client))
            {
                peoples[client].Add(account);
            }
            else
            {
                list.Add(account);
                peoples.Add(client, list);
            }

            AddDictionaryInFile();
        }

        private void AddDictionaryInFile()
        {
            string path = Path.Combine("C:", "Users", "37377", "Documents", "GitHub", "DexPractice", "BankSystem", "DataPerson");

            using (FileStream fileStream = new FileStream($"{path}\\Dictionary.txt", FileMode.Append))
            {
                byte[] array;
                string result = "";
                foreach (var item in peoples)
                {
                    result = $"Имя - {item.Key.Name}, Возраст - {item.Key.Age} лет, Паспорт - {item.Key.Passport}\n";

                    for (int i = 0; i < item.Value.Count; i++)
                    {
                        result += $"Аккаунт: {i + 1})Сумма - {item.Value[i].Money}, Валюта - {item.Value[i].CurrencyType.Name}\n";
                    }
                }
                array = Encoding.Default.GetBytes(result);
                fileStream.Write(array, 0, array.Length);
            }
        }

        public Dictionary<Client, List<Account>> CreateDictionaryFromFile(string path, string fileName) // недоделано
        {
            Dictionary<Client, List<Account>> dictionary = new Dictionary<Client, List<Account>>();
            List<Account> listAccount = new List<Account>();

            using (FileStream fileStream = File.OpenRead($"{path}\\{fileName}.txt"))
            {
                byte[] array = new byte[fileStream.Length];
                fileStream.Read(array, 0, array.Length);
                var lines = Encoding.Default.GetString(array);

                string[] arrayFile = lines.Split('\n');

                for (int i = 0; i < arrayFile.Length; i++)
                {
                    string[] arrayResult = arrayFile[i].Split(new[] { ' ', '-', ',' });

                    var client = new Client() { Name = arrayResult[3], Age = Convert.ToInt32(arrayResult[8]), Passport = Convert.ToInt32(arrayResult[14])};

                    //
                    //здесь должно быть добавление аккаунтов
                    //

                    dictionary.Add(client, listAccount);

                }
                return dictionary;
            }
        }

    }
}
