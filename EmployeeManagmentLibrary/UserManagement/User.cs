using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagmentLibrary
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public bool IsAdmin()
        {
            if (UserId == 0 && Username == "Admin")
            {
                return true;
            }
            return false;
        }
    }
}
