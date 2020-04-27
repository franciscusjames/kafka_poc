
using System;
using System.Threading;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

public class KafkaConsumerService : IConsumerService
{
    private readonly IConfiguration configuration;

    private readonly ILogger<KafkaConsumerService> logger;

    private ConsumerConfig consumerConfig;

    private IConsumer<Ignore, string> consumerBuilder;


    private CancellationTokenSource cancellationTokenSource;

    public KafkaConsumerService(IConfiguration _configuration, ILogger<KafkaConsumerService> _logger)
    {
        configuration = _configuration;
        logger = _logger;
        cancellationTokenSource = new CancellationTokenSource();
    }

    private void ConfigureCancelEvent()
    {
        Console.CancelKeyPress += (_, e) =>
        {
            e.Cancel = true;
            cancellationTokenSource.Cancel();
        };
    }

    public void Subscribe(string topic, string group)
    {
        if (consumerBuilder != null) throw new Exception("Already subscribed!");

        consumerConfig = new ConsumerConfig
        {
            GroupId = group,
            BootstrapServers = configuration["MessageBrokerServer"]
        };

        consumerBuilder = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();

        consumerBuilder.Subscribe(topic);
        ConfigureCancelEvent();

        logger.LogInformation
        (
            "Application {Application} - Subscribed to topic: {ConsumerTopic}",
            configuration["Application"],
            configuration["ConsumerTopic"]
        );
    }

    public ConsumerResult ConsumeMessage()
    {
        try
        {
            var consumeResult = consumerBuilder.Consume(cancellationTokenSource.Token);
            logger.LogInformation("Application {Application} - Message received", configuration["Application"]);

            return new ConsumerResult
            {
                Message = consumeResult.Value,
                Offset = consumeResult.Offset.Value
            };
        }
        catch (OperationCanceledException ex)
        {
            consumerBuilder.Close();
            logger.LogError("Application {Application} Error: {Message}", configuration["Application"], ex.Message);

            return new ConsumerResult
            {
                Message = String.Empty,
                Offset = 0
            };
        }
    }
}