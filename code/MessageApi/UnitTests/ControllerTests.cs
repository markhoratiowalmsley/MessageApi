using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Data;
using Data.Factory;
using Data.Models;
using MessageApi.Controllers;
using MessageApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Service;

namespace UnitTests
{
    public class ControllerTests
    {
        [TestCase("First")]
        public async Task AddMessage(string messageContent)
        {
            var service = new MessageService(new MemoryMessageRepository(), new MessageFactory());
            var logger = new Mock<ILogger<MessageController>>();
            var controller = new MessageController(logger.Object, service);


            // Act
            var response = await controller.Post(new MessageDto { MessageContent = messageContent });

            var action = response as CreatedAtActionResult;

            Assert.That(action, Is.Not.Null);
            var message = action.Value as IMessage;

            Assert.That(message, Is.Not.Null);
            Assert.That(message.MessageContent, Is.EqualTo(messageContent));
        }

        [TestCase("Second")]
        public async Task GetById(string messageContent)
        {
            var service = new MessageService(new MemoryMessageRepository(), new MessageFactory());
            var logger = new Mock<ILogger<MessageController>>();
            var controller = new MessageController(logger.Object, service);


            // Act
            var response = await controller.Post(new MessageDto { MessageContent = messageContent });
            var action = response as CreatedAtActionResult;
            var message = action.Value as IMessage;

            var getById = await controller.GetById(message.Id.ToString());

            var result = getById as OkObjectResult;

            Assert.That(result, Is.Not.Null);

            var dto = result.Value as MessageDto;

            Assert.That(dto.MessageContent, Is.EqualTo(messageContent));
        }

        [TestCase("Third")]
        public async Task Get(string messageContent)
        {
            var service = new MessageService(new MemoryMessageRepository(), new MessageFactory());
            var logger = new Mock<ILogger<MessageController>>();
            var controller = new MessageController(logger.Object, service);


            // Act
            var response = await controller.Post(new MessageDto { MessageContent = messageContent });
            var action = response as CreatedAtActionResult;

            var get = await controller.Get();

            var result = get as OkObjectResult;

            Assert.That(result, Is.Not.Null);

            var messages = result.Value as IEnumerable<MessageDto>;

            Assert.That(messages.First().MessageContent, Is.EqualTo(messageContent));
        }
    }
}
