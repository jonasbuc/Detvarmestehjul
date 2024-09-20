using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Detvarmestehjul.Model;

namespace Detvarmestehjul.Services
{
    public class RentalRepo
    {
        private readonly SqlConnection _connection;
       

        public RentalRepo(string connectionString )
        {
            _connection = new SqlConnection(connectionString);
        }

        public void Add(Rental rental)
        {
            string query = "INSERT INTO Rentals (BikeID, UserID, StartTime, EndTime, TotalCost) VALUES (@BikeID, @UserID, @StartTime, @EndTime, @TotalCost)";
            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@BikeID", rental.BikeID);
                cmd.Parameters.AddWithValue("@UserID", rental.UserID);
                cmd.Parameters.AddWithValue("@StartTime", rental.StartTime);
                cmd.Parameters.AddWithValue("@EndTime", rental.EndTime);
                cmd.Parameters.AddWithValue("@TotalCost", rental.TotalCost);
                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
        }

        public IEnumerable<Rental> GetRentalsByUser(int userId)
        {
            string query = "SELECT * FROM Rentals WHERE UserID = @userId";
            List<Rental> rentals = new List<Rental>();
            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@userId", userId);
                _connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rentals.Add(new Rental
                        {
                            RentalID = (int)reader["RentalID"],
                            BikeID = (int)reader["BikeID"],
                            UserID = (int)reader["UserID"],
                            StartTime = (DateTime)reader["StartTime"],
                            EndTime = (DateTime)reader["EndTime"],
                            TotalCost = (decimal)reader["TotalCost"]
                        });
                    }
                }
                _connection.Close();
            }
            return rentals;
        }
    }
}
