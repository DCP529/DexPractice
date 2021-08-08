using BankSystem.Exceptions;
using System;

namespace BankSystem.Models
{
    public class Client : IPerson
    {
        public string Name { get; set; }
        private int age;
        public int Age
        {
            set
            {
                try
                {
                    if (value > 18 || value <= 0)
                    {
                        throw new AgeLimitException("Недопустимое значение для возраста");
                    }
                    else
                    {
                        age = value;
                    }
                }
                catch (AgeLimitException ageLimitException)
                {
                    Console.WriteLine(ageLimitException.Message);
                }
            }
            get { return age; }
        }
        public int Passport { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is Client))
            {
                return false;
            }

            Client client = (Client)obj;

            return client.Name == Name
                && client.age == age
                && client.Passport == Passport;
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode() + age.GetHashCode() + Passport.GetHashCode();
        }
    }
}
