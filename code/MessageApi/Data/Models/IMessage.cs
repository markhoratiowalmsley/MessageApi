namespace Data.Models
{
    public interface IMessage : IDataObject
    {
        public string MessageContent { get; set; }
    }
}
