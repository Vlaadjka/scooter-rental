using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScooterRental.Controllers.Requests
{
    public class IncomeRequest
    {
        public int? year { get; set; }
        public bool includeNotCompletedRentals { get; set; }
    }
}
