using ScooterRental.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScooterRental.Services
{
    public class RentalCompanyService : IRentalCompany
    {
        private IScooterService scooterService;
        private ConcurrentBag<RentalOrder> rentalOrders;

        public RentalCompanyService(IScooterService scooterService)
        {
            this.scooterService = scooterService;
            rentalOrders = new ConcurrentBag<RentalOrder>();
            Name = "Awesome rental company";
        }

        public string Name { get; }

        public decimal CalculateIncome(int? year, bool includeNotCompletedRentals)
        {
            throw new NotImplementedException();
        }

        public decimal EndRent(string id)
        {
            Scooter scooter = scooterService.GetScooterById(id);
            if ((scooter != null) && (scooter.IsRented == true))
            {
                RentalOrder order = rentalOrders.Single(o => ((o.DateTo == null) && (id.Equals(o.ScooterId))));

                order.DateTo = DateTime.UtcNow;
                scooter.IsRented = false;

                // TODO: calculate rental price
                return 0m;
            }
            else
            {
                return 0m;
            }
        }

        public void StartRent(string id)
        {
            Scooter scooter = scooterService.GetScooterById(id);
            if ((scooter != null) && (scooter.IsRented == false))
            {
                rentalOrders.Add(new RentalOrder
                {
                    ScooterId = id,
                    DateFrom = DateTime.UtcNow,
                    DateTo = null,
                    PricePerMinute = scooter.PricePerMinute
                });

                scooter.IsRented = true;
            }

        }
    }
}
