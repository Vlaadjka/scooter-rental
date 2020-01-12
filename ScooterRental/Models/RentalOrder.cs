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
    }
}
