using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagmentLibrary
{
    public interface IEmployee
    { 
        decimal CalculateWage();

        List<IEmployee> GetSuborinates();

        void SetSubordinates(List<IEmployee> subordinates);

        EmployeeInfo GetEmployeeInfo();

        bool CanHaveSubordinates();

        EmployeeType GetEmployeeType();
    }
}
