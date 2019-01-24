using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLibrary;
namespace EmployeeManagmentLibrary
{
    public class EmployeeInfoConverter
    {
        public EmployeeEntity Convert(EmployeeType type, EmployeeInfo employeeInfo)
        {
            return new EmployeeEntity
            {
                Id = employeeInfo.Id,
                FirstName = employeeInfo.FirstName,
                LastName = employeeInfo.LastName,
                WageRate = employeeInfo.WageRate,
                EnrollmentDate = employeeInfo.EnrollmentDate,
                ChiefId = employeeInfo.ChiefId,
                Position = type.ToString()
            };
            
        }
    }
}
