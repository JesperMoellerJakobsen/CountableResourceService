using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Model;
using Domain.Services;
using Moq;
using NUnit.Framework;
using Repositories.RabbitMQStub;
using Repositories.Repositories;

namespace UnitTests
{
    [TestFixture]
    public class CounterServiceTest
    {
        private Mock<ICounterRepository> _counterRepo;
        private Mock<IRabbitMqStub> _rabbitStub;
        private CounterService _subjectUnderTest;
        private string _counterVersion;
        private byte[] _counterVersionBytes;

        [SetUp]
        public void Setup()
        {
            _counterVersion = "AAAAAAAAB9M=";
            _counterVersionBytes = Convert.FromBase64String(_counterVersion);
            _counterRepo = new Mock<ICounterRepository>();
            _rabbitStub = new Mock<IRabbitMqStub>();
            _subjectUnderTest = new CounterService(_counterRepo.Object, _rabbitStub.Object);
        }

        [Test]
        public void GetCounterCallsRepository()
        {
            //Arrange
            ICounter returnObject = new Counter
            {
                Id = 1,
                Value = 100,
                Version = _counterVersionBytes
            };

            var returnTask = Task.FromResult(returnObject);
            _counterRepo.Setup(x => x.GetCounter()).Returns(returnTask);

            //Act
            var result = _subjectUnderTest.GetCounter();

            //Assert
            Assert.That(result.IsCompletedSuccessfully, Is.True);
            Assert.That(result.Result.Id, Is.EqualTo(returnObject.Id));
            Assert.That(result.Result.Value, Is.EqualTo(returnObject.Value));
            Assert.That(result.Result.Version, Is.EqualTo(returnObject.Version));
            _counterRepo.Verify(x => x.GetCounter(), Times.Exactly(1));
        }

        [Test]
        public void IncrementCounterCallsRepository()
        {
            //Arrange
            var returnTask = Task.FromResult(true);
            _counterRepo.Setup(x => x.TryIncrement(It.IsAny<byte[]>())).Returns(returnTask);

            //Act
            var result = _subjectUnderTest.TryIncrement(_counterVersionBytes);

            //Assert
            Assert.That(result.IsCompletedSuccessfully, Is.True);
            Assert.That(result.Result, Is.True);
            _counterRepo.Verify(x => x.TryIncrement(It.Is<byte[]>(y => y.SequenceEqual(_counterVersionBytes))), Times.Exactly(1));
            _rabbitStub.Verify(x => x.PublishChangedCounterState(), Times.Exactly(1));
        }

        [Test]
        public void DecrementCounterCallsRepository()
        {
            //Arrange
            var returnTask = Task.FromResult(true);
            _counterRepo.Setup(x => x.TryIncrement(It.IsAny<byte[]>())).Returns(returnTask);

            //Act
            var result = _subjectUnderTest.TryIncrement(_counterVersionBytes);

            //Assert
            Assert.That(result.IsCompletedSuccessfully, Is.True);
            Assert.That(result.Result, Is.True);
            _counterRepo.Verify(x => x.TryIncrement(It.Is<byte[]>(y => y.SequenceEqual(_counterVersionBytes))), Times.Exactly(1));
            _rabbitStub.Verify(x => x.PublishChangedCounterState(), Times.Exactly(1));
        }
    }
}
