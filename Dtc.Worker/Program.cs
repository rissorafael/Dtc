using Dct.Infra.IoC;
using Dtc.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    })
    .ConfigureServices((context, services) =>
    {
        var configuration = context.Configuration;

        // Registra dependências
        services.AddInfrastructure(configuration);

        // Registra HostedService
        services.AddHostedService<KafkaConsumerService>();
    })
    .Build();

await host.RunAsync();
