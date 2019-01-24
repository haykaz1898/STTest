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
    public partial class EmployeeMonitoringSystem : Form
    {
        private EmployeeHandler _handler;
        private User _user;
        private UserAuthentication _userAuthentication;

        public EmployeeMonitoringSystem(User user)
        {
            _handler = EmployeeHandler.Init();
            _userAuthentication = new UserAuthentication();
            _user = user;
            InitializeComponent();

            if (_user.IsAdmin())
            {
                addUserToolStripMenuItem.Enabled = true;
                addUserToolStripMenuItem.Visible = true;
                
            }
            else
            {
                addUserToolStripMenuItem.Enabled = false;
                addUserToolStripMenuItem.Visible = false;
            }
            LoadDatabase();
        }

        private void AddRow(IEmployee employee)
        {
            DataGridViewRow r = new DataGridViewRow();
            IEmployee chief = _handler.FindChief(employee);
            EmployeeInfo chiefInfo = null;
            if (chief != null)
            {
                chiefInfo = chief.GetEmployeeInfo();
            }
            string chiefFullName = chief != null ? chiefInfo.FirstName + " " + chiefInfo.LastName : "";

            EmployeeInfo employeeInfo = employee.GetEmployeeInfo();
            string[] s = new string[]{
           
            employeeInfo.FirstName,
            employeeInfo.LastName,
            employee.GetType().Name,
            employeeInfo.WageRate.ToString(),
            employee.CalculateWage().ToString(),
            employeeInfo.EnrollmentDate.ToString(),
            chiefFullName
            };
                       
            dataGridView1.Rows.Add(s);
        }

        private void LoadDatabase()
        {
            if (_user.IsAdmin())
            {
                _handler.Refresh();
                dataGridView1.Rows.Clear();
                var list = _handler.GetEmployees().ToList();

                foreach (var i in list)
                {
                    AddRow(i);
                }
                textBox1.Text = _handler.GetTotalWage().ToString();
            }
            else
            {
                dataGridView1.Rows.Clear();
                var employee = _handler.GetEmployeeByUser(_user);
                var subordinates = employee.GetSuborinates().ToList();

                AddRow(employee);
                foreach (var i in subordinates)
                {
                    AddRow(i);
                }
            }
        }
        
        public void AddUser()
        {
            AddUser dlg = new AddUser();

            var employeesWithNoUser = _handler.GetEmployeesWithNoUser();
            List<string> employeesFullName = new List<string>();

            foreach (var emp in employeesWithNoUser)
            {
                employeesFullName.Add(emp.GetEmployeeInfo().FirstName + " " + emp.GetEmployeeInfo().LastName);
            }
            dlg.SetUserBox(employeesFullName);

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string[] fullName = dlg.User.Split(' ');
                var employee = _handler.FindEmployeeByName(fullName[0], fullName[1]);
                var employeeInfo = employee.GetEmployeeInfo();

                var user = new User
                {
                    UserId = employeeInfo.Id,
                    Username = dlg.Username,
                    Password = dlg.Password
                };

                _userAuthentication.AddUser(user);
            }

        }
        
        private void Insert()
        {
            Insert dlg = new Insert();
            List<string> positions = new List<string>(Enum.GetNames(typeof(EmployeeType)));
            dlg.SetGroupBox(positions);

            List<string> chiefsFullName = new List<string>();
            foreach (var emp in _handler.GetChiefs())
            {
                chiefsFullName.Add(emp.GetEmployeeInfo().FirstName + " " + emp.GetEmployeeInfo().LastName);
            }
            dlg.SetChiefBox(chiefsFullName);

            if (dlg.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            if (!dlg.SetEnrollmentDateManual()){
                MessageBox.Show("Please enter the correct date", "Wrong date",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                dlg.Close();
                return;
            }

            if (!dlg.SetWageRate())
            {
                MessageBox.Show("Please enter the correct number", "Wrong number",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                dlg.Close();
                return;
            }
            
            


            string[] fn = dlg.Chief.Split(' ');
            IEmployee chief = _handler.FindEmployeeByName(fn[0], fn[1]);
            int _chiefId = chief.GetEmployeeInfo().Id;

           

            EmployeeInfo employeeInfo = new EmployeeInfo
            {
                FirstName = dlg.FirstName,
                LastName = dlg.LastName,
                EnrollmentDate = dlg.EnrollmentDate,
                ChiefId = _chiefId,
                WageRate = dlg.WageRate
            };


            _handler.InsertEmployee(dlg.Type,employeeInfo);
            LoadDatabase();
        }

        private void Insert_Click(object sender, EventArgs e)
        {
            Insert();
        }
        
        private void InsertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Insert();
        }

        private void ReloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadDatabase();
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadDatabase();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AddUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddUser();
        }
    }
}
