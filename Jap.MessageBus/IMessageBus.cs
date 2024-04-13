namespace Jap.MessageBus
{
    public interface IMessageBus
    {
        Task PublishMessage(BaseMassage massage, string topicName);
    }
}
