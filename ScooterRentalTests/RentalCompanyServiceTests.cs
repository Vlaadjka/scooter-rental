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
        private Scooter scooter1, scooter2, scooter3;

        public RentalCompanyServiceTests()
        {
            scooter1 = new Scooter("Ninebot3656743", 0.10m);
            scooter2 = new Scooter("Ninebot9775456", 0.07m);
            scooter3 = new Scooter("Xiaomi563467", 0.09m);
        }

        [SetUp]
        public void TestSetup()
        {
            // Create new service for each test to have empty company and scooter list            
            scooterService = new ScooterService();
            rentalCompanyService = new RentalCompanyService(scooterService);

            // Add new scooters
            scooterService.AddScooter(scooter1.Id, scooter1.PricePerMinute);
            scooterService.AddScooter(scooter2.Id, scooter2.PricePerMinute);
            scooterService.AddScooter(scooter3.Id, scooter3.PricePerMinute);
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

        [Test]
        public void EndRent_ShouldReturnRentalPrice()
        {
            Scooter scooter = scooterService.GetScooterById(scooter1.Id);
            rentalCompanyService.StartRent(scooter.Id);
            Assert.AreEqual(scooter.PricePerMinute, rentalCompanyService.EndRent(scooter.Id));
        }

        [Test]
        public void CalculateIncome_ShouldReturnIncomeForAllScooters()
        {
            rentalCompanyService.StartRent(scooter1.Id);
            rentalCompanyService.EndRent(scooter1.Id);

            rentalCompanyService.StartRent(scooter2.Id);

            rentalCompanyService.StartRent(scooter3.Id);

            // Price will be calculated for 1 minute as it is minimum rent period
            Assert.AreEqual(
                scooter1.PricePerMinute + scooter2.PricePerMinute + scooter3.PricePerMinute,
                rentalCompanyService.CalculateIncome(null, true));
        }

        [Test]
        public void CalculateIncome_ShouldReturnIncomeForEndedRentalsOnly()
        {
            rentalCompanyService.StartRent(scooter1.Id);
            rentalCompanyService.EndRent(scooter1.Id);

            rentalCompanyService.StartRent(scooter2.Id);

            // Price will be calculated for 1 minute as it is minimum rent period
            Assert.AreEqual(
                scooter1.PricePerMinute,
                rentalCompanyService.CalculateIncome(null, false));
        }

        [Test]
        public void CalculateIncome_ShouldReturnIncomeForSpecificYear()
        {
            rentalCompanyService.StartRent(scooter1.Id);
            rentalCompanyService.EndRent(scooter1.Id);

            rentalCompanyService.StartRent(scooter2.Id);
            rentalCompanyService.EndRent(scooter2.Id);

            // Price will be calculated for 1 minute as it is minimum rent period
            Assert.AreEqual(
                scooter1.PricePerMinute + scooter2.PricePerMinute,
                rentalCompanyService.CalculateIncome(DateTime.UtcNow.Year, true));
        }

        [Test]
        public void CalculateIncome_ShouldReturnZeroForYearsWithoutIncome()
        {
            rentalCompanyService.StartRent(scooter1.Id);
            rentalCompanyService.EndRent(scooter1.Id);

            // Price will be calculated for 1 minute as it is minimum rent period
            Assert.AreEqual(0, rentalCompanyService.CalculateIncome(DateTime.UtcNow.Year - 1, true));
        }
    }
}
