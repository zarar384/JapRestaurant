namespace Jap.MessageBus
{
    public interface IMessageBus
    {
        Task PublishMessage(BaseMessage massage, string topicName);
    }
}
