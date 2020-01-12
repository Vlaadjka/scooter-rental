using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScooterRental.Models
{
    public class RentalOrder
    {
        public string ScooterId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public decimal PricePerMinute { get; set; }
        public decimal RentalPrice { get; set; }

        private IEnumerable<Tuple<DateTime, DateTime>> SplitByDays(DateTime from, DateTime to)
        {
            while (from < to)
            {
                DateTime tomorrow = from.AddDays(1).Date;
                if (tomorrow >= to)
                {
                    yield return Tuple.Create(from, to);
                    from = to;
                }
                else
                {
                    yield return Tuple.Create(from, tomorrow);
                    from = tomorrow;
                }
            }
        }

        private decimal CalculateDayRentalPrice(Tuple<DateTime, DateTime> tuple)
        {
            TimeSpan span = tuple.Item2.Subtract(tuple.Item1);
            // Minimum rental minutes is 1
            double dailyRentalMinutes = Math.Ceiling(span.TotalMinutes);
            decimal dailyRentalPrice = Decimal.Parse(dailyRentalMinutes.ToString()) * PricePerMinute;

            // Maximum daily rental price is 20
            return dailyRentalPrice <= 20m ? dailyRentalPrice : 20m;
        }

        public decimal CalculateRentalPrice()
        {
            RentalPrice = SplitByDays(DateFrom, DateTo.HasValue ? DateTo.Value : DateTime.UtcNow)
                .Aggregate(0m, (rentalPrice, next) => rentalPrice + CalculateDayRentalPrice(next));

            return RentalPrice;
        }
    }
}
