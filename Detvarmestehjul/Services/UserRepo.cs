using Detvarmestehjul.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Detvarmestehjul.Services.Repo;

namespace Detvarmestehjul.Services
{
    public class UserRepo : IRepository<User>
    {
        private readonly SqlConnection _connection;
        //private readonly string connectionString = "Server=localhost;Database=Hotwheels;Integrated Security=True;;Encrypt=False";

        public UserRepo(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public void Add(User user)
        {
            string query = "INSERT INTO Users (FirstName, LastName, Email, MembershipDate) VALUES (@FirstName, @LastName, @Email, @MembershipDate)";
            SqlCommand cmd = null;

            try
            {
                cmd = new SqlCommand(query, _connection);
                cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                cmd.Parameters.AddWithValue("@LastName", user.LastName);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@MembershipDate", user.MembershipDate);

                _connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"An error occurred while adding the user: {ex.Message}");
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
        }

        public User Get(int id)
        {
            string query = "SELECT * FROM Users WHERE UserID = @id";
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            User user = null;

            try
            {
                cmd = new SqlCommand(query, _connection);
                cmd.Parameters.AddWithValue("@id", id);

                _connection.Open();
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    user = new User
                    {
                        UserID = (int)reader["UserID"],
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"],
                        Email = (string)reader["Email"],
                        MembershipDate = (DateTime)reader["MembershipDate"]
                    };
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"An error occurred while retrieving the user: {ex.Message}");
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
            return user;
        }

        public IEnumerable<User> GetAll()
        {
            string query = "SELECT * FROM Users";
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            List<User> users = new List<User>();

            try
            {
                cmd = new SqlCommand(query, _connection);
                _connection.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    User user = new User
                    {
                        UserID = (int)reader["UserID"],
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"],
                        Email = (string)reader["Email"],
                        MembershipDate = (DateTime)reader["MembershipDate"]
                    };
                    users.Add(user);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"An error occurred while retrieving users: {ex.Message}");
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
            return users;
        }

        public void Update(User user)
        {
            string query = "UPDATE Users SET FirstName = @FirstName, LastName = @LastName, Email = @Email WHERE UserID = @id";
            SqlCommand cmd = null;

            try
            {
                cmd = new SqlCommand(query, _connection);
                cmd.Parameters.AddWithValue("@id", user.UserID);
                cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                cmd.Parameters.AddWithValue("@LastName", user.LastName);
                cmd.Parameters.AddWithValue("@Email", user.Email);

                _connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
               Console.WriteLine($"An error occurred while updating the user: {ex.Message}");
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM Users WHERE UserID = @id";
            SqlCommand cmd = null;

            try
            {
                cmd = new SqlCommand(query, _connection);
                cmd.Parameters.AddWithValue("@id", id);

                _connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"An error occurred while deleting the user: {ex.Message}");
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
        }
    }
}
