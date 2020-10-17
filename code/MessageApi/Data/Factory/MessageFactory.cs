using System;
using Data.Models;

namespace Data.Factory
{
    public class MessageFactory : IMessageFactory
    {
        public IMessage Build(string messageContent)
        {
            return new Message { Id = Guid.NewGuid(), MessageContent = messageContent };
        }
    }
}
