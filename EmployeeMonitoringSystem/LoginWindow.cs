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
using System.Threading;
namespace EmployeeMonitoringSystem
{
    public partial class LoginWindow : Form
    {
        public string Login
        {
            get
            {
                return loginBox.Text;
            }
            set
            {
                loginBox.Text = value;
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
        public LoginWindow()
        {
            InitializeComponent();
        }
    }
}
