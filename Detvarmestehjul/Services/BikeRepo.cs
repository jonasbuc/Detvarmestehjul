using System;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Detvarmestehjul.Services.Repo;
using Detvarmestehjul.Model;
using System.Data;

namespace Detvarmestehjul.Services;

public class BikeRepo : IRepository<Bike>
{
    private readonly SqlConnection _connection;
    
    public BikeRepo(string connectionString)
    {
        _connection = new SqlConnection(connectionString);
    }

    public void Add(Bike bike)
    {
        string query = "INSERT INTO Bikes (Model, Location, Status, BatteryLevel) VALUES (@Model, @Location, @Status, @BatteryLevel)";

        try
        {
            using (var cmd = new SqlCommand(query, _connection))
            {
                // Tilføj parametre eksplicit med typer for bedre performance og sikkerhed
                cmd.Parameters.Add("@Model", SqlDbType.VarChar).Value = bike.Model;
                cmd.Parameters.Add("@Location", SqlDbType.VarChar).Value = bike.Location;
                cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = bike.Status;
                cmd.Parameters.Add("@BatteryLevel", SqlDbType.Int).Value = bike.BatteryLevel;

                _connection.Open();
                cmd.ExecuteNonQuery();
            }
        }
        catch (SqlException ex)
        {
            // Håndter eventuelle SQL-specifikke fejl
            //Console.WriteLine($"An error occurred while adding the bike: {ex.Message}");
        }
        finally
        {
            // Sørg for, at forbindelsen altid lukkes
            if (_connection.State == System.Data.ConnectionState.Open)
            {
                _connection.Close();
            }
        }
    }

    public Bike Get(int id)
    {
        string query = "SELECT * FROM Bikes WHERE BikeID = @id";
        Bike bike = null;

        using (var cmd = new SqlCommand(query, _connection))
        {
            cmd.Parameters.AddWithValue("@id", id);
            _connection.Open();

            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    bike = new Bike
                    {
                        BikeID = (int)reader["BikeID"],
                        Model = (string)reader["Model"],
                        Location = (string)reader["Location"],
                        Status = (bool)reader["Status"],
                        BatteryLevel = (int)reader["BatteryLevel"]
                    };
                }
            }
            _connection.Close();
        }

        return bike;
    }

    public IEnumerable<Bike> GetAll()
    {
        
        List<Bike> bikes = new List<Bike>();
        string query = "SELECT * FROM Bikes";

        using (var cmd = new SqlCommand(query, _connection))
        {
            _connection.Open();
            

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Bike bike = new Bike()
                    {
                        BikeID = (int)reader["BikeID"],
                        Model = (string)reader["Model"],
                        Location = (string)reader["Location"],
                        Status = (bool)reader["Status"],
                        BatteryLevel = (int)reader["BatteryLevel"]
                    };
                    bikes.Add(bike);
                }
            }
            _connection.Close();
        }

        return bikes;
    }

    public void Update(Bike bike)
    {
        string query = "UPDATE Bikes SET Model = @Model, Location = @Location, Status = @Status, BatteryLevel = @BatteryLevel WHERE BikeID = @id";

        using (var cmd = new SqlCommand(query, _connection))
        {
            cmd.Parameters.AddWithValue("@id", bike.BikeID);
            cmd.Parameters.AddWithValue("@Model", bike.Model);
            cmd.Parameters.AddWithValue("@Location", bike.Location);
            cmd.Parameters.AddWithValue("@Status", bike.Status);
            cmd.Parameters.AddWithValue("@BatteryLevel", bike.BatteryLevel);

            _connection.Open();
            cmd.ExecuteNonQuery();
            _connection.Close();
        }
    }

    public void Delete(int id)
    {
        string query = "DELETE FROM Bikes WHERE BikeID = @id";

        using (var cmd = new SqlCommand(query, _connection))
        {
            cmd.Parameters.AddWithValue("@id", id);

            _connection.Open();
            cmd.ExecuteNonQuery();
            _connection.Close();
        }
    }
}