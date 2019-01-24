using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagmentLibrary
{ 
    public class EmployeeCreator : IEmployeeFactory
    {
        public IEmployee CreateEmployee(EmployeeType type, EmployeeInfo employeeInfo)
        {
            if (type == EmployeeType.Employee)
            {
                return new Employee(employeeInfo);
            }
            if (type == EmployeeType.Manager)
            {
                return new Manager(employeeInfo);
            }
            if (type == EmployeeType.Salesman)
            {
                return new Salesman(employeeInfo);
            }
            return null;
        }
    }
}
