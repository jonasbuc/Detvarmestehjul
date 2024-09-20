using Detvarmestehjul.Controller;
using Detvarmestehjul.Model;
using Detvarmestehjul.Services;

namespace Detvarmestehjul

{
    public class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=localhost\\SQLEXPRESS;Database=Hotwheels;Trusted_Connection=True;TrustServerCertificate=True;";


            var bikeRepo = new BikeRepo(connectionString);
            var userRepo = new UserRepo(connectionString);
            var rentalRepo = new RentalRepo(connectionString);

            var controller = new BikeRentalController(bikeRepo, userRepo, rentalRepo);
            bool exit = false;

            while (!exit)
            {
                // Menu
                Console.WriteLine("===== Bike Rental System =====");
                Console.WriteLine("1. Add a new bike");
                Console.WriteLine("2. Add a new user");
                Console.WriteLine("3. Start a new rental");
                Console.WriteLine("4. Show all bikes");
                Console.WriteLine("5. Show all users");
                Console.WriteLine("6. Show all rentals");
                Console.WriteLine("0. Exit");
                Console.Write("Enter your choice:");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddBike(controller);
                        break;
                    case "2":
                        AddUser(controller);
                        break;
                    case "3":
                        StartRental(controller);
                        break;
                    //case "4":
                    //    ShowAllBikes(controller);
                    //    break;
                    //case "5":
                    //    ShowAllUsers(controller);
                    //    break;
                    //case "6":
                    //    ShowAllRentals(controller);
                        //break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
                Console.WriteLine(); // Blank line for better formatting
            }

            Console.WriteLine("Exiting program. Goodbye!");
        }

        // Metoder til hver menu-operation

        static void AddBike(BikeRentalController controller)
        {
            Console.WriteLine("Enter bike model: ");
            string model = Console.ReadLine();

            Console.WriteLine("Enter bike location: ");
            string location = Console.ReadLine();

            Console.WriteLine("Enter bike status (Available/Unavailable): ");
            bool status = bool.Parse(Console.ReadLine());

            Console.WriteLine("Enter battery level (0-100): ");
            int batteryLevel = int.Parse(Console.ReadLine());

            Bike bike = new Bike
            {
                Model = model,
                Location = location,
                Status = status,
                BatteryLevel = batteryLevel
            };

            controller.AddBike(bike);
            Console.WriteLine("Bike added successfully.");
        }

        static void AddUser(BikeRentalController controller)
        {
            Console.WriteLine("Enter user's first name: ");
            string firstName = Console.ReadLine();

            Console.WriteLine("Enter user's last name: ");
            string lastName = Console.ReadLine();

            Console.WriteLine("Enter user's email: ");
            string email = Console.ReadLine();

            User user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                MembershipDate = DateTime.Now // Registrerer datoen for medlemskab
            };

            controller.AddUser(user);
            Console.WriteLine("User added successfully.");
        }

        static void StartRental(BikeRentalController controller)
        {
            Console.WriteLine("Enter bike ID for rental: ");
            int bikeId = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter user ID for rental: ");
            int userId = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter rental start time (yyyy-MM-dd HH:mm:ss): ");
            DateTime startTime = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Enter rental end time (yyyy-MM-dd HH:mm:ss): ");
            DateTime endTime = DateTime.Parse(Console.ReadLine());

            decimal totalCost = CalculateRentalCost(startTime, endTime);

            Rental rental = new Rental
            {
                BikeID = bikeId,
                UserID = userId,
                StartTime = startTime,
                EndTime = endTime,
                TotalCost = totalCost
            };

            controller.StartRental(rental);
            Console.WriteLine("Rental started successfully.");
        }

       

        // Hjælpemetode til at beregne lejeprisen
        static decimal CalculateRentalCost(DateTime startTime, DateTime endTime)
        {
            TimeSpan rentalDuration = endTime - startTime;
            decimal hourlyRate = 10.0m; // F.eks. 10 enheder pr. time
            return (decimal)rentalDuration.TotalHours * hourlyRate;
        }
    }

}



