using Data.Models;

namespace Data.Factory
{
    public interface IMessageFactory
    {
        IMessage Build(string messageContent);
    }
}