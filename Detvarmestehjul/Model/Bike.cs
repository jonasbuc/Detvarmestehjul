using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detvarmestehjul.Model
{
    public class Bike
    {
        public int BikeID { get; set; }
        public string Model { get; set; }
        public string Location { get; set; }
        public bool Status { get; set; }
        public int BatteryLevel { get; set; } // Indikerer om Cyklen er elektrisk

        public Bike()
        {

        }

        public Bike(int id, string model, string location, bool IsRented, int batteryLevel)
        {
            BikeID = id;
            Model = model;
            Location = location;
            Status = IsRented;
            BatteryLevel = batteryLevel;
        }

        public Bike(string model, string location, bool IsRented, int batteryLevel)
        {
            Model = model;
            Location = location;
            Status = IsRented;
            BatteryLevel = batteryLevel;
        }

        public override string ToString()
        {
            return $"Id: {BikeID}, Model: {Model}, Location: {Location}, Status: {Status}, Battery: {BatteryLevel}";
        } 
    }

}
