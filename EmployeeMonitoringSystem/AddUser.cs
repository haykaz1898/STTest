using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeMonitoringSystem
{
    public partial class AddUser : Form
    {
        public string Username
        {
            get
            {
                return usernameBox.Text;
            }
            set
            {
                usernameBox.Text = value;
            }
        }

        public string Password
        {
            get
            {
                return passwordBox.Text;
            }
            set
            {
                passwordBox.Text = value;
            }
        }

        public string User
        {
            get
            {
                return userBox.SelectedValue.ToString();
            }
            set { }
        }
        
        public void SetUserBox(IEnumerable<string> users)
        {
            userBox.DataSource = users;
        }

        public AddUser()
        {
            InitializeComponent();
        }
    }
}
