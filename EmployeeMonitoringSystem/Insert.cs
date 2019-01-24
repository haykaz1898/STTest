using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EmployeeManagmentLibrary;

namespace EmployeeMonitoringSystem
{
    public partial class Insert : Form
    {
        public string FirstName
        {
            get
            {
                return firstNameBox.Text;
            }
            private set { }
        }
        public string LastName
        {
            get
            {
                return lastNameBox.Text;
            }
            private set { }
        }
        public EmployeeType Type
        {
            get
            {
                EmployeeType employeeType;
                Enum.TryParse<EmployeeType>(groupBox.SelectedValue.ToString(), out employeeType);
                return employeeType;
            }
            set
            {

            }
        }
        public DateTime EnrollmentDate { get; set; }
        public string Chief
        {
            get
            {
                return chiefBox.SelectedValue.ToString();
            }
            set
            {

            }
        }
        public decimal WageRate { get; set; }

        public Insert()
        {
            InitializeComponent();


            SetEnrollmentDate();

            dateBox.Text = EnrollmentDate.ToString();
        }

        public void SetEnrollmentDate()
        {
            EnrollmentDate = DateTime.Now;
        }

        public bool SetEnrollmentDateManual()
        {
            DateTime dateTime;

            if (!(DateTime.TryParse(dateBox.Text, out dateTime))){
                return false;
            }
            EnrollmentDate = dateTime;
            return true;
        }

        public bool SetWageRate()
        {

            if (!(decimal.TryParse(wageRateBox.Text, out decimal wageRate)) || wageRate < 0)
            {
                WageRate = wageRate;
                return false;
            }
            return true;
        }

        public void SetChiefBox(IEnumerable<string> chiefsFullName)
        {
            chiefBox.DataSource = chiefsFullName;
        }

        public void SetGroupBox(IEnumerable<string> positions)
        {
            groupBox.DataSource = positions;
        }

    }
}
