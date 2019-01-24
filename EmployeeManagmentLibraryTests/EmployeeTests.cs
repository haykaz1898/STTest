using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeeManagmentLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagmentLibrary.Tests
{
    [TestClass()]
    public class EmployeeTests
    {
        [TestMethod()]
        public void CalculateWageTest()
        {
            EmployeeInfo employeeInfo = new EmployeeInfo();
            string date = "12.12.2016";
            DateTime dt;
            DateTime.TryParse(date,out dt);

            employeeInfo.WageRate = 16340;
            employeeInfo.EnrollmentDate = dt;
            Employee employee = new Employee(employeeInfo);


            decimal expectedResult = 17320.4m;
            decimal actualResult = employee.CalculateWage();
            Assert.AreEqual(expectedResult,actualResult);
        }
        [TestMethod()]
        public void CalculateWageTest2()
        {
            EmployeeInfo employeeInfo = new EmployeeInfo();
            string date = "12.12.2008";
            DateTime dt;
            DateTime.TryParse(date, out dt);

            employeeInfo.WageRate = 16340;
            employeeInfo.EnrollmentDate = dt;
            Employee employee = new Employee(employeeInfo);


            decimal expectedResult = 21242;
            decimal actualResult = employee.CalculateWage();
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod()]

        public void CalculateWageTest3()
        {
            EmployeeInfo employeeInfo = new EmployeeInfo();
            string date = "12.12.1999";
            DateTime dt;
            DateTime.TryParse(date, out dt);

            employeeInfo.WageRate = 16340;
            employeeInfo.EnrollmentDate = dt;
            Employee employee = new Employee(employeeInfo);


            decimal expectedResult = 21242;
            decimal actualResult = employee.CalculateWage();
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}