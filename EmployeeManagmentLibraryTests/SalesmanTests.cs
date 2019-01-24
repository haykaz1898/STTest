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
    public class SalesmanTests
    {
        [TestMethod()]
        public void CalculateWageTest()
        {
            //Chief
            EmployeeInfo chiefInfo = new EmployeeInfo();
            string date = "12.12.2008";
            DateTime dt;
            DateTime.TryParse(date, out dt);

            chiefInfo.Id = 1;
            chiefInfo.EnrollmentDate = dt;
            chiefInfo.ChiefId = -1;
            chiefInfo.WageRate = 34659;
            Salesman Chief = new Salesman(chiefInfo);
            decimal expected = 38124.9m;
            decimal actual = Chief.CalculateWage();

            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void CalculateWageTest2()
        {
            //Chief
            EmployeeInfo chiefInfo = new EmployeeInfo();
            string date = "12.12.2008";
            DateTime dt;
            DateTime.TryParse(date, out dt);

            chiefInfo.Id = 1;
            chiefInfo.EnrollmentDate = dt;
            chiefInfo.ChiefId = -1;
            chiefInfo.WageRate = 34659;
            Salesman Chief = new Salesman(chiefInfo);

            ////First Lvl 
            EmployeeInfo firstLvlInfo = new EmployeeInfo();

            date = "12.12.2014";
            DateTime.TryParse(date, out dt);

            firstLvlInfo.Id = 2;
            firstLvlInfo.EnrollmentDate = dt;
            firstLvlInfo.ChiefId = 1;
            firstLvlInfo.WageRate = 27560;
            Salesman firstLvl = new Salesman(firstLvlInfo);
            
            //Second Lvl
            EmployeeInfo secondLvlInfo = new EmployeeInfo();

            date = "12.12.2015";
            DateTime.TryParse(date, out dt);

            secondLvlInfo.Id = 3;
            secondLvlInfo.EnrollmentDate = dt;
            secondLvlInfo.ChiefId = 1;
            secondLvlInfo.WageRate = 24985;
            Salesman secondLvl = new Salesman(secondLvlInfo);
            
            //Setting Subordinates
            List<IEmployee> Chiefsubordinates = new List<IEmployee>();
            Chiefsubordinates.Add(firstLvl);
            Chief.SetSubordinates(Chiefsubordinates);

            List<IEmployee> firstLvlSubordinates = new List<IEmployee>();
            firstLvlSubordinates.Add(secondLvl);
            firstLvl.SetSubordinates(firstLvlSubordinates);

            decimal expected = 38288.32246095m;
            decimal actual = Chief.CalculateWage();
            
            Assert.AreEqual(expected,actual);
        }
        
        [TestMethod()]
        public void CalculateWageTest3()
        {
            //Chief
            EmployeeInfo chiefInfo = new EmployeeInfo();
            string date = "12.12.2008";
            DateTime dt;
            DateTime.TryParse(date, out dt);

            chiefInfo.Id = 1;
            chiefInfo.EnrollmentDate = dt;
            chiefInfo.ChiefId = -1;
            chiefInfo.WageRate = 34659;
            Salesman Chief = new Salesman(chiefInfo);

            //First Lvl 
            EmployeeInfo firstLvlInfo = new EmployeeInfo();

            date = "12.12.2014";
            DateTime.TryParse(date, out dt);

            firstLvlInfo.Id = 2;
            firstLvlInfo.EnrollmentDate = dt;
            firstLvlInfo.ChiefId = 1;
            firstLvlInfo.WageRate = 27560;
            Salesman firstLvl = new Salesman(firstLvlInfo);

            //First Lvl 2
            EmployeeInfo firstLvlInfo2 = new EmployeeInfo();

            date = "12.12.2015";
            DateTime.TryParse(date, out dt);

            firstLvlInfo2.Id = 3;
            firstLvlInfo2.EnrollmentDate = dt;
            firstLvlInfo2.ChiefId = 2;
            firstLvlInfo2.WageRate = 24985;
            Salesman firstLvl2 = new Salesman(firstLvlInfo2);

            //Setting Subordinates
            List<IEmployee> Chiefsubordinates = new List<IEmployee>();
            Chiefsubordinates.Add(firstLvl);
            Chiefsubordinates.Add(firstLvl2);
            Chief.SetSubordinates(Chiefsubordinates);


            decimal expected = 38288.09085m;
            decimal actual = Chief.CalculateWage();

            Assert.AreEqual(expected, actual);
        }
    }
}