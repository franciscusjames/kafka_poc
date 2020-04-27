using System;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

public class KafkaProducerService : IProducerService
{
    private readonly IConfiguration configuration;

    private ProducerConfig producerConfiguration;

    private IProducer<string, string> producer;

    private string produceStatus;
    private readonly ILogger<KafkaProducerService> logger;

    public KafkaProducerService(IConfiguration _configuration, ILogger<KafkaProducerService> _logger)
    {
        configuration = _configuration;
        logger = _logger;
        Configure();
    }

    private void Configure()
    {
        producerConfiguration = new ProducerConfig
        {
            BootstrapServers = configuration["MessageBrokerServer"]
        };

        producer = new ProducerBuilder<string, string>(producerConfiguration).Build();
    }

    public string SendMessage(string topic, string key, object value)
    {
        try
        {
            logger.LogInformation($"Sending message to {topic}... ");

            var deliveryResult = producer.ProduceAsync(
                topic,
                new Message<string, string> { Key = key, Value = JsonConvert.SerializeObject(value) }
            );

            producer.Flush(timeout: TimeSpan.FromSeconds(1));

            produceStatus = deliveryResult.Status.ToString();

            if (produceStatus == "RanToCompletion")
            {
                logger.LogInformation("Message delivered!");
                return deliveryResult.Result.Status.ToString();
            }
            else
            {
                Console.WriteLine();
                return produceStatus;
            }
        }
        catch (ProduceException<Null, string> e)
        {
            logger.LogError($"Delivery failed: {e.Error.Reason}");
            return PersistenceStatus.NotPersisted.ToString();
        }
    }
}