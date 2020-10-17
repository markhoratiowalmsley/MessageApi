using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;

namespace Data
{
    public class MemoryMessageRepository : IRepository<IMessage>
    {
        private readonly IList<IMessage> _messages;

        public MemoryMessageRepository()
        {
            _messages = new List<IMessage>();
        }

        public async Task<IMessage> Insert(IMessage item)
        {
            if(_messages.Any(x=>x.Id == item.Id))
            {
                throw new InvalidOperationException($"Id already exists {item.Id}");
            }

            _messages.Add(item);
            return await Task.FromResult(item);
        }

        public async Task<IEnumerable<IMessage>> SelectAll()
        {
            return await Task.FromResult(_messages);
        }

        public async Task<IMessage> SelectById(Guid id)
        {
            return await Task.FromResult(_messages.SingleOrDefault(x => x.Id == id));
        }
    }
}
