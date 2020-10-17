using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Models;

namespace Service
{
    public interface IMessageService
    {
        Task<IMessage> AddMessage(string messageContent);
        Task<IEnumerable<IMessage>> SelectAll();
        Task<IMessage> SelectById(Guid id);
    }
}
