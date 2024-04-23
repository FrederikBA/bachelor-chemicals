using System.Text.Json;
using Chemicals.Core.Interfaces.Integration;
using Confluent.Kafka;

namespace Chemicals.Infrastructure.Producers;


public class SyncProducer : ISyncProducer   
{
    private readonly IProducer<string, string> _producer;
    private const string BootstrapServers = "localhost:9092";

    public SyncProducer()
    {
        var config = new ProducerConfig { BootstrapServers = BootstrapServers };
        _producer = new ProducerBuilder<string, string>(config).Build();
    }
    
    public async Task ProduceAsync<T>(string topic, T value)
    {
        var message = new Message<string, string>
        {
            Key = Guid.NewGuid().ToString(),
            Value = JsonSerializer.Serialize(value)
        };
        await _producer.ProduceAsync(topic, message);
    }


    public void Dispose()
    {
        _producer.Dispose();
    }
}