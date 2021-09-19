using System.Threading.Tasks;
using Domain.Model;
using Repositories.RabbitMQStub;
using Repositories.Repositories;

namespace Domain.Services
{
    public class CounterService : ICounterService
    {
        private readonly ICounterRepository _counterRepository;
        private readonly IRabbitMqStub _rabbitMqStub;

        public CounterService(ICounterRepository counterRepository, IRabbitMqStub rabbitMqStub)
        {
            _counterRepository = counterRepository;
            _rabbitMqStub = rabbitMqStub;
        }

        public async Task<ICounter> GetCounter()
        {
            return await _counterRepository.GetCounter();
        }

        public async Task<bool> TryIncrement(byte[] currentVersion)
        {
            var result = await _counterRepository.TryIncrement(currentVersion);

            if (result)
                _rabbitMqStub.PublishChangedCounterState();

            return result;
        }

        public async Task<bool> TryDecrement(byte[] currentVersion)
        {
            var result = await _counterRepository.TryDecrement(currentVersion);

            if (result)
                _rabbitMqStub.PublishChangedCounterState();

            return result;
        }
    }
}
