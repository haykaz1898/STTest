using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagmentLibrary
{
    public class Salesman : IEmployee
    {
        private EmployeeInfo EmployeeInfo { get; set; }
        private List<IEmployee> _subordinates;

        public Salesman(EmployeeInfo employeeInfo)
        {
            EmployeeInfo = employeeInfo;
        }

        public decimal CalculateWage()
        {
            var tmp = DateTime.Now - EmployeeInfo.EnrollmentDate;
            int years = (int)tmp.TotalDays / 365;
            decimal bonus = 0.01m * years;
            if (bonus > 0.35m)
            {
                bonus = 0.35m;
            }
            decimal wage = EmployeeInfo.WageRate * (1 + bonus);
            List<EmployeeInfo> processed = new List<EmployeeInfo>();
            processed.Add(EmployeeInfo);
            if (_subordinates != null)
            {
                foreach (var sub in _subordinates)
                {
                    wage += BonusFromSubordinates(sub, 0.003m, processed);
                }
            }
            return wage;
        }

        private decimal BonusFromSubordinates(IEmployee employee, decimal bonusTerm, List<EmployeeInfo> processed)
        {
            if (processed.Contains(employee.GetEmployeeInfo()))
            {
                return 0;
            }
            processed.Add(employee.GetEmployeeInfo());
            decimal w = employee.CalculateWage() * bonusTerm;
            var subs = employee.GetSuborinates();
            if (subs != null)
            {
                foreach (var sub in subs)
                {
                    w += BonusFromSubordinates(sub, bonusTerm, processed);
                }
            }
            return w;
        }
        
        public EmployeeInfo GetEmployeeInfo()
        {
            return EmployeeInfo;
        }

        public List<IEmployee> GetSuborinates()
        {
            return _subordinates;
        }

        public void SetSubordinates(List<IEmployee> subordinates)
        {
            this._subordinates = subordinates;
        }

        public bool CanHaveSubordinates()
        {
            return true;
        }

        public EmployeeType GetEmployeeType()
        {
            return EmployeeType.Salesman;
        }
    }
}
