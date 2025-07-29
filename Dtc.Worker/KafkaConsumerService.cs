using System.Text.Json;
using Confluent.Kafka;
using Dtc.Domain.Entities;
using Dtc.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace Dtc.Worker
{
    public class KafkaConsumerService : BackgroundService
    {

        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IConfiguration _configuration;
        private readonly string _bootstrapServers;
        private readonly string _topic;
        private readonly string _groupId;
        private readonly ILogger<KafkaConsumerService> _logger;

        public KafkaConsumerService(IServiceScopeFactory serviceScopeFactory,
                                    IConfiguration configuration,
                                    ILogger<KafkaConsumerService> logger
            )
        {
            _serviceScopeFactory = serviceScopeFactory;
            _configuration = configuration;

            _bootstrapServers = _configuration["Kafka:BootstrapServers"]!;
            _topic = _configuration["Kafka:Topic"]!;
            _groupId = _configuration["Kafka:GroupId"] ?? "default-group";
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = _bootstrapServers,
                GroupId = _groupId,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            consumer.Subscribe(_topic);

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {

                    var result = consumer.Consume(stoppingToken);

                    _logger.LogInformation("Mensagem recebida do Kafka: {Mensagem}", result.Message.Value);

                    var produto = JsonSerializer.Deserialize<Product>(result.Message.Value);

                    var product = new ProductMongo
                    {
                        Name = produto.Name,
                        Price = produto.Price
                    };

                    // Salva no MongoDB
                    using var scope = _serviceScopeFactory.CreateScope();
                    try
                    {
                        var queueService = scope.ServiceProvider.GetRequiredService<IProductsMongoApplication>();
                        await queueService.CreateAsync(product);

                        _logger.LogInformation("Produto salvo no MongoDB: {Nome}", product.Name);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Erro ao salvar o produto no MongoDB.");
                        Console.WriteLine($"[Worker] - Erro ao consumir a fila {produto}");
                    }

                    Console.WriteLine($"Produto salvo no MongoDB: {produto.Name}");
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Worker cancelado.");
                Console.WriteLine("Worker cancelado");

            }
            finally
            {
                consumer.Close();
                _logger.LogInformation("Consumer Kafka fechado.");
                Console.WriteLine("Consumer fechado");
            }
        }
    }
}
