using System;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.Factory;
using NUnit.Framework;
using Service;

namespace UnitTests
{
    public class ServiceTests
    {
        private IMessageService _service;

        [SetUp]
        public void Setup()
        {
            _service = new MessageService(new MemoryMessageRepository(), new MessageFactory());
        }

        [TestCase("ContentA")]
        public async Task AddMessagesReturnsId(string messageContent)
        {
            var message = await _service.AddMessage(messageContent);

            Assert.That(message.Id, Is.Not.EqualTo(Guid.Empty));
        }

        [TestCase("ContentB")]
        public async Task SelectAllReturnsMessages(string messageContent)
        {
            var message = await _service.AddMessage(messageContent);

            var all = await _service.SelectAll();

            var single = all.SingleOrDefault();
            Assert.That(single, Is.Not.Null);
            Assert.That(single.Id, Is.EqualTo(message.Id));
            Assert.That(single.MessageContent, Is.EqualTo(messageContent));
        }

        [TestCase("ContentB")]
        public async Task SelectByIdReturnsMessage(string messageContent)
        {
            var message = await _service.AddMessage(messageContent);

            var selected = await _service.SelectById(message.Id);

            Assert.That(selected, Is.Not.Null);
            Assert.That(selected.Id, Is.EqualTo(message.Id));
            Assert.That(selected.MessageContent, Is.EqualTo(messageContent));
        }
    }
}
