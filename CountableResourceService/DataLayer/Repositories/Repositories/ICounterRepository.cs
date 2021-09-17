using Domain.Model;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public interface ICounterRepository
    {
        public Task<ICounter> GetCounter();
        public Task<bool> TryIncrement(byte[] clientCounterVersion);
        public Task<bool> TryDecrement(byte[] clientCounterVersion);
    }
}
