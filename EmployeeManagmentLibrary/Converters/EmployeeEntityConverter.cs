using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLibrary;

namespace EmployeeManagmentLibrary
{
    public class EmployeeEntityConverter
    {
        private IEmployeeFactory _employeeFactory;

        public EmployeeEntityConverter()
        {
            _employeeFactory = new EmployeeCreator();
        }

        public IEmployee Convert(EmployeeEntity employeeEntity)
        {
            if (employeeEntity != null)
            {
                var employeeInfo = new EmployeeInfo
                {
                    Id = employeeEntity.Id,
                    FirstName = employeeEntity.FirstName,
                    LastName = employeeEntity.LastName,
                    WageRate = employeeEntity.WageRate,
                    EnrollmentDate = employeeEntity.EnrollmentDate,
                    ChiefId = employeeEntity.ChiefId
                };

                EmployeeType employeeType;

                if (Enum.TryParse(employeeEntity.Position, true, out employeeType))
                {
                    return _employeeFactory.CreateEmployee(employeeType, employeeInfo);
                }
            }
            // неопознная позиция, записываем в лог 
            return null;
        }

        public IEnumerable<IEmployee> Convert(IEnumerable<EmployeeEntity> entities)
        {
            List<IEmployee> employees = new List<IEmployee>();
            foreach (var e in entities)
            {
                employees.Add(Convert(e));    
            }

            return employees;
        }
    }
}
