using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detvarmestehjul.Model
{
    public class Rental
    {
        public int RentalID { get; set; }
        public int BikeID { get; set; }
        public int UserID { get; set; }
        public DateTime StartTime { get; set; } 
        public DateTime EndTime { get; set; } 
        public decimal TotalCost { get; set; }

        public Rental()
        {
                
        }

        public Rental(int id, int bikeId, int userId, DateTime startTime, DateTime endTime, decimal totalCost)
        {
            RentalID = id;
            BikeID = bikeId;
            UserID = userId;
            StartTime = startTime;
            EndTime = endTime;
            TotalCost = totalCost;
        }

        public Rental(int bikeId, int userId, DateTime startTime, DateTime endTime, decimal totalCost)
        {
            BikeID = bikeId;
            UserID = userId;
            StartTime = startTime;
            EndTime = endTime;
            TotalCost = totalCost;
        }

        public override string ToString()
        {
            return $"RentalID: {RentalID}, BikeID: {BikeID}, : UserID {UserID}, Rental Start time: {StartTime}, Rental end time: {EndTime}";
        }
    }
}
