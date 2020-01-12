using NUnit.Framework;
using ScooterRental.Models;
using ScooterRental.Services;
using System.Collections.Generic;

namespace ScooterRentalTests
{
    [TestFixture]
    public class ScooterServiceTests
    {
        private IScooterService scooterService;
        private Scooter scooter1, scooter2;

        public ScooterServiceTests()
        {
            scooter1 = new Scooter("Ninebot3656743", 0.08m);
            scooter2 = new Scooter("Xiaomi563467", 0.07m);
        }

        [SetUp]
        public void TestSetup()
        {
            // Create new service for each test to have empty scooter list
            scooterService = new ScooterService();
        }

        [Test]
        public void AddScooter_ShouldNotThrowExceptions()
        {
            scooterService.AddScooter(scooter1.Id, scooter1.PricePerMinute);
            scooterService.AddScooter(scooter2.Id, scooter2.PricePerMinute);
        }

        [Test]
        public void GetScooterById_ShouldReturnScooter()
        {
            scooterService.AddScooter(scooter1.Id, scooter1.PricePerMinute);

            Scooter scooter = scooterService.GetScooterById(scooter1.Id);

            Assert.NotNull(scooter);
            Assert.AreEqual(scooter.Id, scooter1.Id);
            Assert.AreEqual(scooter.PricePerMinute, scooter1.PricePerMinute);
        }

        [Test]
        public void GetScooterById_ShouldReturnOnlyExistingScooters()
        {
            Scooter scooter = scooterService.GetScooterById("NotExistingScooterId");

            Assert.Null(scooter);
        }

        [Test]
        public void GetScooters_ShouldReturnAllScooters()
        {
            scooterService.AddScooter(scooter1.Id, scooter1.PricePerMinute);
            scooterService.AddScooter(scooter2.Id, scooter2.PricePerMinute);

            IList<Scooter> scooters = scooterService.GetScooters();

            Assert.AreEqual(scooters.Count, 2);
        }

        [Test]
        public void RemoveScooter_ShouldNotThrowExceptions()
        {
            scooterService.RemoveScooter("RandomScooter");

            Assert.Pass();
        }

        [Test]
        public void RemoveScooter_ShouldRemoveScooter()
        {
            scooterService.AddScooter(scooter1.Id, scooter1.PricePerMinute);

            scooterService.RemoveScooter(scooter1.Id);

            IList<Scooter> scooters = scooterService.GetScooters();
            Assert.AreEqual(scooters.Count, 0);
        }
    }
}