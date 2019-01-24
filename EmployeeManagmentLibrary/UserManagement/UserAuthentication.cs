using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DatabaseLibrary;
namespace EmployeeManagmentLibrary
{
    public class UserAuthentication
    {
        Database _database = new Database();
        
        public User Authenticate(string username,string password)
        {
            SeedUsers();
            string passwordHash = ComputeHash(password);
            var u = _database.Authentication(username, passwordHash);
            if (u != null)
            {
                return new User
                {
                    UserId = u.UserId,
                    Username = u.Username,
                    Password = u.Password
                };
            }
            return null;
        }

        public void SeedUsers()
        {
            if (_database.IsEmpty())
            {
                string passwordHash = ComputeHash("Admin");
                _database.InsertUser(0, "Admin", passwordHash);
            }
        }

        private static string ComputeHash(string password)
        {
            string passwordHash;
            using (var md5 = MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(password);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                passwordHash = sb.ToString();
            }

            return passwordHash;
        }

        public void AddUser(User user)
        {
            string passwordHash = ComputeHash(user.Password);
            _database.InsertUser(user.UserId, user.Username, passwordHash);
        }

        public void ChangePassword(User user, string newPassword)
        {
            _database.ChangePassword(user.UserId, user.Username, user.Password, newPassword);
        }

    }
}
