using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScooterRental.Controllers.Requests
{
    public class ScooterRequest
    {
        public string Id { get; set; }
        public decimal PricePerMinute { get; set; }
    }
}
