using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data;
using Data.Factory;
using Data.Models;

namespace Service
{
    public class MessageService : IMessageService
    {
        private readonly IRepository<IMessage> _repository;
        private readonly IMessageFactory _factory;

        public MessageService(
            IRepository<IMessage> repository,
            IMessageFactory factory)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public async Task<IMessage> AddMessage(string messageContent)
        {
            var message = _factory.Build(messageContent);
            return await _repository.Insert(message);
        }

        public async Task<IEnumerable<IMessage>> SelectAll()
        {
            return await _repository.SelectAll();
        }

        public async Task<IMessage> SelectById(Guid id)
        {
            return await _repository.SelectById(id);
        }
    }
}
