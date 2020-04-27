
using Confluent.Kafka;

public interface IProducerService
{
    string SendMessage(string topic, string key, object value);
}