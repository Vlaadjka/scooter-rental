using NUnit.Framework;
using ScooterRental.Models;
using ScooterRental.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScooterRentalTests
{
    [TestFixture]
    class RentalCompanyServiceTests
    {
        private IRentalCompany rentalCompanyService;
        private IScooterService scooterService;
        private Scooter scooter1;

        public RentalCompanyServiceTests()
        {
            scooter1 = new Scooter("Ninebot3656743", 0.10m);
        }

        [SetUp]
        public void TestSetup()
        {
            // Create new service for each test to have empty company and scooter list            
            scooterService = new ScooterService();
            rentalCompanyService = new RentalCompanyService(scooterService);

            // Add new scooter
            scooterService.AddScooter(scooter1.Id, scooter1.PricePerMinute);
        }

        [Test]
        public void StartRent_ShouldSetIsRentedTrue()
        {
            Scooter scooter = scooterService.GetScooterById(scooter1.Id);
            Assert.IsFalse(scooter.IsRented);

            rentalCompanyService.StartRent(scooter.Id);
            Assert.IsTrue(scooter.IsRented);
        }

        [Test]
        public void EndRent_ShouldSetIsRentedFalse()
        {
            Scooter scooter = scooterService.GetScooterById(scooter1.Id);
            Assert.IsFalse(scooter.IsRented);

            rentalCompanyService.StartRent(scooter.Id);
            Assert.IsTrue(scooter.IsRented);

            rentalCompanyService.EndRent(scooter.Id);
            Assert.IsFalse(scooter.IsRented);
        }
    }
}
