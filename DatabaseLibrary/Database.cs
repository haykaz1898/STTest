using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Data.Entity;

namespace DatabaseLibrary
{
      
    public class Database
    {
        private readonly SQLiteConnection _connection;
        private readonly String _connectionString = "Data Source=database.sqlite3; PRAGMA journal_mode=WAL";

        public Database()
        {
            _connection = new SQLiteConnection(_connectionString);
            if (!File.Exists("./database.sqlite3"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
            }
        }

        private EmployeeEntity ReadFromDb(SQLiteDataReader dataReader)
        {
            EmployeeEntity entity = new EmployeeEntity();
            try
            {
                entity.Id = Convert.ToInt32(dataReader["Id"]);
                entity.FirstName = Convert.ToString(dataReader["first_name"]);
                entity.LastName = Convert.ToString(dataReader["last_name"]);
                entity.EnrollmentDate = Convert.ToDateTime(dataReader["enrollment_date"]);
                entity.WageRate = Convert.ToDecimal(dataReader["wage"]);
                entity.ChiefId = Convert.ToInt32(dataReader["chiefId"]);
                entity.Position = Convert.ToString(dataReader["position"]);
            }
            catch (InvalidDataException)
            {
                
            }
            return entity;
        }

        public void Insert(EmployeeEntity entity)
        {
            string query = "INSERT INTO Employee('chiefId','first_name','last_name','enrollment_date','position','wage')" +
                                        "VALUES(@chiefId,@first_name,@last_name,@enrollment_date,@position,@wage)";

            using (SQLiteConnection c = new SQLiteConnection(_connection))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(query, c))
                {
                    cmd.Parameters.AddWithValue("@chiefId", entity.ChiefId);
                    cmd.Parameters.AddWithValue("@first_name", entity.FirstName.ToString());
                    cmd.Parameters.AddWithValue("@last_name", entity.LastName.ToString());
                    cmd.Parameters.AddWithValue("@enrollment_date", entity.EnrollmentDate.ToString());
                    cmd.Parameters.AddWithValue("@position", entity.Position);
                    cmd.Parameters.AddWithValue("@wage", entity.WageRate);
                    cmd.ExecuteNonQuery();
                }                  
            }           
        }

        public void InsertUser(int userId, string username,string password)
        {
            string insertUserQuery = "INSERT INTO User('userId','username','password') " +
                "VALUES(@userId,@username,@password);";

            using (SQLiteConnection c = new SQLiteConnection(_connection))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(insertUserQuery, c))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void ChangePassword(int userId, string username, string password, string newPassword)
        {
            string query = "UPDATE User SET password = @newPassword where username = @username AND userId = @userID AND password = @password";
            using (SQLiteConnection c = new SQLiteConnection(_connection))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(query, c))
                {
                    cmd.Parameters.AddWithValue("@newPassword", newPassword);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@newPassword", newPassword);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public bool IsEmpty()
        {
            string query = "SELECT Count(userId) as cnt FROM User";
            using (SQLiteConnection c = new SQLiteConnection(_connection))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(query, c))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        rdr.Read();
                        return Convert.ToInt32(rdr["cnt"]) == 0;
                    }
                }
            }
        }

        public UserEntity Authentication(string username,string password)
        {

            string query = "SELECT userId FROM user WHERE username = @username AND password = @password";
            
            using (SQLiteConnection c = new SQLiteConnection(_connection))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(query, c))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            rdr.Read();
                            int userID = Convert.ToInt32(rdr["userId"]);
                            return new UserEntity
                            {
                                UserId = userID,
                                Username = username,
                                Password = password
                            };
                        }
                    }
                }
            }

            return null;
        }

        public IEnumerable<EmployeeEntity> Read()
        {
            List<EmployeeEntity> entities = new List<EmployeeEntity>();
            string query = "SELECT * FROM Employee;";

            using (SQLiteConnection c = new SQLiteConnection(_connection))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(query, c))
                {
                    using(SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read()){
                                entities.Add(ReadFromDb(rdr));
                            }
                        }
                    }
                }
            }

            return entities;
        }

        public IEnumerable<EmployeeEntity> ReadEntitiesWithNoUser()
        {
            string query = "SELECT * FROM Employee LEFT JOIN User " +
                "ON Employee.id = User.userId WHERE User.userId IS NUll";

            List<EmployeeEntity> EntitiesWithNoUser = new List<EmployeeEntity>();
            using (SQLiteConnection c = new SQLiteConnection(_connection))
            {
                c.Open();
                using(SQLiteCommand cmd = new SQLiteCommand(query, c))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                EntitiesWithNoUser.Add(ReadFromDb(rdr));
                            }
                        }
                    }
                }
            }
            return EntitiesWithNoUser;
        }
    }
}
