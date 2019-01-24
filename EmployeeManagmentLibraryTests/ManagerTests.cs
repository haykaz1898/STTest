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
    public class ManagerTests
    {
        [TestMethod()]
        public void CalculateWageTest()
        {
            string date = "12.12.2014";
            DateTime dt;
            DateTime.TryParse(date, out dt);

            EmployeeInfo managerInfo = new EmployeeInfo();

            managerInfo.EnrollmentDate = dt;
            managerInfo.WageRate = 27560;

            Manager manager = new Manager(managerInfo);

            decimal expected = 33072;
            decimal actual = manager.CalculateWage();

            Assert.AreEqual(expected,actual);
        }
        [TestMethod()]
        public void CalculateWageTest2()
        {
            string date = "12.12.2016";
            DateTime dt;
            DateTime.TryParse(date, out dt);

            EmployeeInfo managerInfo = new EmployeeInfo();
            managerInfo.EnrollmentDate = dt;
            managerInfo.WageRate = 24567;
            Manager manager = new Manager(managerInfo);

            date = "12.12.2017";
            DateTime.TryParse(date, out dt);
            EmployeeInfo subInfo1 = new EmployeeInfo();

            subInfo1.EnrollmentDate = dt;
            subInfo1.WageRate = 23456;
            Manager sub1 = new Manager(subInfo1);

            date = "12.12.2018";
            DateTime.TryParse(date, out dt);
            EmployeeInfo subInfo2 = new EmployeeInfo();

            subInfo2.EnrollmentDate = dt;
            subInfo2.WageRate = 15678;
            Manager sub2 = new Manager(subInfo2);

            List<IEmployee> subordinates = new List<IEmployee>();
            subordinates.Add(sub1);
            subordinates.Add(sub2);
            manager.SetSubordinates(subordinates);

            decimal sub1Wage = 23456 + 23456 * 0.05m;// = 24628.8m;
            decimal sub2Wage = 15678; 
            decimal managerWage = 24567 + 24567 * 2 * 0.05m + sub1Wage * 0.005m + sub2Wage * 0.005m;
            //24567 + 2456.7 + 123.144 + 78.39 = 27225.234
            decimal expected = managerWage;// 27225.234m;
            decimal actual = manager.CalculateWage();
            Assert.AreEqual(expected,actual);
        }
        
    }
}