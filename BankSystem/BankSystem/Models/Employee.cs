

namespace BankSystem.Models
{
    public class Employee : IPerson
    {
        public string Name { get ; set; }
        public int Age { get; set; }
        public int Passport { get; set; }
        public decimal Wage { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is Employee))
            {
                return false;
            }

            Employee employee = (Employee)obj;
            return employee.Name == Name &&
                employee.Age == Age &&
                employee.Passport == Passport &&
                employee.Wage == Wage;
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode() + Age.GetHashCode() + Passport.GetHashCode() + Wage.GetHashCode();
        }
    }
}
