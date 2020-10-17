namespace Data.Models
{
    public class Message : DataObject, IMessage
    {
        public string MessageContent { get; set; }
    }
}
