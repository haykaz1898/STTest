using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagmentLibrary
{
    public class Manager : IEmployee
    {
        private EmployeeInfo EmployeeInfo { get; set; }
        private List<IEmployee> _subordinates;

        public Manager(EmployeeInfo employeeInfo)
        {
            EmployeeInfo = employeeInfo;
        }

        public decimal CalculateWage()
        {
            var tmp = DateTime.Now - EmployeeInfo.EnrollmentDate;
            int years = (int)tmp.TotalDays / 365;
            decimal bonus = 0.05m * years;
            if (bonus > 0.4m)
            {
                bonus = 0.4m;
            }
            decimal wage = EmployeeInfo.WageRate * (1 + bonus);
            if (_subordinates != null)
            {
                foreach (var sub in _subordinates)
                {
                    wage += sub.CalculateWage() * 0.005m;
                }
            }
            return wage;
        }

        public bool CanHaveSubordinates()
        {
            return true;
        }

        public EmployeeInfo GetEmployeeInfo()
        {
            return EmployeeInfo;
        }

        public EmployeeType GetEmployeeType()
        {
            return EmployeeType.Manager;
        }

        public List<IEmployee> GetSuborinates()
        {
            return _subordinates;
        }

        public void SetSubordinates(List<IEmployee> subordinates)
        {
            this._subordinates = subordinates;
        }
    }
}
