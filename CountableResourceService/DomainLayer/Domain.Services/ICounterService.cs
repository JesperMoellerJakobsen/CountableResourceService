using System.Threading.Tasks;
using Domain.Model;

namespace Domain.Services
{
    public interface ICounterService
    {
        public Task<ICounter> GetCounter();
        public Task<bool> TryIncrement(byte[] clientCounterVersion);
        public Task<bool> TryDecrement(byte[] clientCounterVersion);
    }
}
