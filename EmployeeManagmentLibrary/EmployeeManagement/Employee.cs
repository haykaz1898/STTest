using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagmentLibrary
{
    public class Employee : IEmployee
    {
        private EmployeeInfo EmployeeInfo { get; set; }

        public Employee(EmployeeInfo employeeInfo)
        {
            EmployeeInfo = employeeInfo;
            
        }

        public decimal CalculateWage()
        {
            var tmp = DateTime.Now - EmployeeInfo.EnrollmentDate;
            int years = (int)tmp.TotalDays / 365;
            decimal bonus = 0.03m * years;
            if (bonus > 0.3m)
            {
                bonus = 0.3m;
            }
            return EmployeeInfo.WageRate * (1 + bonus);
        }

        public bool CanHaveSubordinates()
        {
            return false;
        }

        public EmployeeInfo GetEmployeeInfo()
        {
            return EmployeeInfo;
        }

        public EmployeeType GetEmployeeType()
        {
            return EmployeeType.Employee;
        }

        public List<IEmployee> GetSuborinates()
        {
            return null;
        }

        public void SetSubordinates(List<IEmployee> subordinates)
        {
            throw new InvalidDataException();
        }
    }
}
