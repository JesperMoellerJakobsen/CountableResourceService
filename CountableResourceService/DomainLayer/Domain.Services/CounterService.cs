using System.Threading.Tasks;
using Domain.Model;
using Repositories.Repositories;

namespace Domain.Services
{
    public class CounterService : ICounterService
    {
        private readonly ICounterRepository _counterRepository;

        public CounterService(ICounterRepository counterRepository)
        {
            _counterRepository = counterRepository;
        }

        public async Task<ICounter> GetCounter()
        {
            return await _counterRepository.GetCounter();
        }

        public async Task<bool> TryIncrement(byte[] currentVersion)
        {
            return await _counterRepository.TryIncrement(currentVersion);
        }

        public async Task<bool> TryDecrement(byte[] currentVersion)
        {
            return await _counterRepository.TryDecrement(currentVersion);
        }
    }
}
