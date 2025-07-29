using Confluent.Kafka;
using Microsoft.Extensions.Configuration;


namespace Dct.Infra.CrossCutting.Kafka
{
    public class KafkaProducer
    {
        private readonly string _bootstrapServers;
        private readonly string _topic;

        public KafkaProducer(IConfiguration configuration)
        {
            _bootstrapServers = configuration["Kafka:BootstrapServers"];
            _topic = configuration["Kafka:Topic"];
        }

        public async Task SendMessageAsync(string message)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = _bootstrapServers
            };

            using var producer = new ProducerBuilder<Null, string>(config).Build();
            await producer.ProduceAsync(_topic, new Message<Null, string> { Value = message });
        }
    }
}
