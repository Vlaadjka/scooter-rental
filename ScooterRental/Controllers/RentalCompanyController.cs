using Microsoft.AspNetCore.Mvc;
using ScooterRental.Controllers.Requests;
using ScooterRental.Controllers.Responses;
using ScooterRental.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScooterRental.Controllers
{
    [Route("v1")]
    [ApiController]
    public class RentalCompanyController
    {
        private IRentalCompany rentalCompanyService;

        public RentalCompanyController(IRentalCompany rentalCompanyService)
        {
            this.rentalCompanyService = rentalCompanyService;
        }

        [HttpPost("rent.start")]
        public void StartRent([FromBody] RentRequest request)
        {
            rentalCompanyService.StartRent(request.ScooterId);
        }

        [HttpPost("rent.end")]
        public RentResponse EndRent([FromBody] RentRequest request)
        {
            return new RentResponse { RentalPrice = rentalCompanyService.EndRent(request.ScooterId) };
        }

        [HttpPost("income.get")]
        public IncomeResponse CalculateIncome([FromBody] IncomeRequest request)
        {
            return new IncomeResponse 
                { 
                    Income = rentalCompanyService.CalculateIncome(
                        request.year, 
                        request.includeNotCompletedRentals) 
                };
        }
    }
}
