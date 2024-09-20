using Detvarmestehjul.Model;
using Detvarmestehjul.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detvarmestehjul.Controller
{
    public class BikeRentalController
    {
        private readonly BikeRepo _bikeRepo;
        private readonly UserRepo _userRepo;
        private readonly RentalRepo _rentalRepo;

        public BikeRentalController(BikeRepo bikeRepo, UserRepo userRepo, RentalRepo rentalRepo)
        {
            _bikeRepo = bikeRepo;
            _userRepo = userRepo;
            _rentalRepo = rentalRepo;
        }

        public void AddBike(Bike bike)
        {
            _bikeRepo.Add(bike);
        }

        public void AddUser(User user)
        {
            _userRepo.Add(user);
        }

        public void StartRental(Rental rental)
        {
            _rentalRepo.Add(rental);
        }
    }
}
