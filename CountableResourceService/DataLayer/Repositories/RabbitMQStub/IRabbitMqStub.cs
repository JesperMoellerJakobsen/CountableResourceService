using Domain.Model;

namespace Repositories.RabbitMQStub
{
    public interface IRabbitMqStub
    {
        public void PublishChangedCounterState();
    }
}
