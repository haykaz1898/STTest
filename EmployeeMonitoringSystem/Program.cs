using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using EmployeeManagmentLibrary; 
namespace EmployeeMonitoringSystem
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var loginWindow = new LoginWindow();
            var authentication = new UserAuthentication();
            while (true) {
                if ((loginWindow.ShowDialog() != DialogResult.OK))
                {
                    return;
                }
                var user = authentication.Authenticate(loginWindow.Login, loginWindow.Password);
                if (user != null)
                {
                    Application.Run(new EmployeeMonitoringSystem(user));
                    return;
                }
                else
                {
                    MessageBox.Show("Wrong username or password");
                }
            }
        }
    }
}
