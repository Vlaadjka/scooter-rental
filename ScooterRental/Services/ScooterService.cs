using ScooterRental.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScooterRental.Services
{
    public class ScooterService : IScooterService
    {
        private ConcurrentDictionary<string, Scooter> scooterRepository;

        public ScooterService()
        {
            scooterRepository = new ConcurrentDictionary<string, Scooter>();
        }

        public void AddScooter(string id, decimal pricePerMinute)
        {
            // Add scooter if there are no scooters with same id
            if (!scooterRepository.TryGetValue(id, out _))
            {
                scooterRepository.TryAdd(id, new Scooter(id, pricePerMinute));
            }
        }

        public Scooter GetScooterById(string scooterId)
        {
            return scooterRepository.GetValueOrDefault(scooterId);
        }

        public IList<Scooter> GetScooters()
        {
            return scooterRepository.Values.ToList<Scooter>();
        }

        public void RemoveScooter(string id)
        {
            scooterRepository.TryRemove(id, out _);
        }
    }
}
