using Microsoft.AspNetCore.Mvc;
using ScooterRental.Controllers.Requests;
using ScooterRental.Models;
using ScooterRental.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScooterRental.Controllers
{
    [Route("v1")]
    [ApiController]
    public class ScooterController : ControllerBase
    {
        private IScooterService scooterService;
        public ScooterController(IScooterService scooterService)
        {
            this.scooterService = scooterService;
        }

        [HttpPost("scooter.getAll")]
        public IEnumerable<Scooter> Get()
        {
            return scooterService.GetScooters();
        }

        [HttpPost("scooter.get")]
        public Scooter GetScooter([FromBody] ScooterRequest request)
        {
            return scooterService.GetScooterById(request.Id);
        }

        [HttpPost("scooter.add")]
        public void AddScooter([FromBody] ScooterRequest request)
        {
            scooterService.AddScooter(request.Id, request.PricePerMinute);
        }

        [HttpPost("scooter.remove")]
        public void RemoveScooter([FromBody] ScooterRequest request)
        {
            scooterService.RemoveScooter(request.Id);
        }
    }
}
