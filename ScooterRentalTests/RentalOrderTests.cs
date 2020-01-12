using NUnit.Framework;
using ScooterRental.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScooterRentalTests
{
    [TestFixture]
    class RentalOrderTests
    {
        [Test]
        public void CalculateRentalPrice_ShouldLimitMaxDailyPrice()
        {
            Assert.AreEqual(0.60m,
                new RentalOrder
                {
                    DateFrom = DateTime.UtcNow.Date,
                    DateTo = DateTime.UtcNow.Date.AddMinutes(4),
                    PricePerMinute = 0.15m
                }.CalculateRentalPrice());

            Assert.AreEqual(20m, 
                new RentalOrder
                {
                    DateFrom = DateTime.UtcNow.Date,
                    DateTo = DateTime.UtcNow.Date.AddHours(10),
                    PricePerMinute = 0.15m
                }.CalculateRentalPrice());

            Assert.AreEqual(20.2m,
                new RentalOrder
                {
                    DateFrom = DateTime.UtcNow.Date,
                    DateTo = DateTime.UtcNow.Date.AddDays(1).AddMinutes(2),
                    PricePerMinute = 0.10m
                }.CalculateRentalPrice());

            Assert.AreEqual(80m,
                new RentalOrder
                {
                    DateFrom = DateTime.UtcNow.Date,
                    DateTo = DateTime.UtcNow.Date.AddDays(3).AddHours(10),
                    PricePerMinute = 0.10m
                }.CalculateRentalPrice());
        }
    }
}
