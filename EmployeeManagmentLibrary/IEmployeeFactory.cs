using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagmentLibrary
{
    public interface IEmployeeFactory
    {
        IEmployee CreateEmployee(EmployeeType type, EmployeeInfo employeeInfo);
    }
}
