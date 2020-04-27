
public interface IConsumerService
{
    void Subscribe(string topic, string group);

    ConsumerResult ConsumeMessage();
}