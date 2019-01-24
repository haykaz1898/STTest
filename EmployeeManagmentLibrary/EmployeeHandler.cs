using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLibrary;
namespace EmployeeManagmentLibrary
{
    public class EmployeeHandler
    {
        private readonly EmployeeEntityConverter _converter;
        private readonly EmployeeCreator _employeeCreator;
        private IEnumerable<IEmployee> _employees;
        private Database _database;
        private static EmployeeHandler _handler;

        private EmployeeHandler()
        {
            _employeeCreator = new EmployeeCreator();
            _converter = new EmployeeEntityConverter();
            _database = new Database();

            Refresh();
            AssignSubordinates();
        }

        public static EmployeeHandler Init()
        {
            if (_handler == null)
            {
                _handler = new EmployeeHandler();
            }
                       
            return _handler;
            
        }

        private void AssignSubordinates()
        {
            foreach (var chief in _employees)
            {
                if (!(chief is Employee))
                {
                    List<IEmployee> subordinates = _employees.Where(e => e.GetEmployeeInfo().ChiefId == chief.GetEmployeeInfo().Id).ToList();

                    chief.SetSubordinates(subordinates);
                }
            }
        }

        public void Refresh()
        {
            var employees = _database.Read();
            _employees = _converter.Convert(employees);
        }

        public void InsertEmployee(EmployeeType type, EmployeeInfo employee)
        {
            EmployeeInfoConverter converter = new EmployeeInfoConverter();
            
            _database.Insert(converter.Convert(type, employee));
        }

        public decimal GetTotalWage()
        {
            decimal totalWage = 0m;

            if (_employees == null)
            {
                return totalWage;
            }

            foreach (var emp in _employees)
            {
                totalWage += emp.CalculateWage();
            }
            return totalWage;
        }

        public IEnumerable<IEmployee> GetEmployees()
        {
            return _employees;
        }

        public IEnumerable<IEmployee> GetChiefs()
        {
            return _employees.Where(e => e.GetEmployeeType() != EmployeeType.Employee);
        }

        public IEmployee FindChief(IEmployee employee)
        {
            return _employees.FirstOrDefault(e => e.GetEmployeeInfo().Id == employee.GetEmployeeInfo().ChiefId);           
        }

        public IEmployee GetEmployeeByUser(User user)
        {
            return _employees.FirstOrDefault(e => e.GetEmployeeInfo().Id == user.UserId);
        }

        public IEmployee FindEmployeeByName(string firstName, string lastName)
        {
            return _employees.FirstOrDefault(e => e.GetEmployeeInfo().FirstName == firstName && e.GetEmployeeInfo().LastName == lastName);
        }

        public IEnumerable<IEmployee> FindSubordinates(IEmployee chief)
        {
            return chief.GetSuborinates();
        }

        public IEnumerable<IEmployee> GetEmployeesWithNoUser()
        {
            List<IEmployee> result = new List<IEmployee>();

            foreach (var emp in _database.ReadEntitiesWithNoUser())
            {
                result.Add(_converter.Convert(emp));
            }
            return result;
        }
    }
}
